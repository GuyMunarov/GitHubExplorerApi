using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using GitHubExplorerApi.Data;
using GitHubExplorerApi.Dtos;
using GitHubExplorerApi.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace GitHubExplorerApi.Controllers
{
    public class ReposController: BaseController
    {
        private IEmailService _emailService;
        IMapper _mapper;
        IUnitOfWork _unitOfWork;
        IExcelService<GitHubRepository> _excelService;
        public ReposController(IEmailService emailService, IMapper mapper, IUnitOfWork unitOfWork, IExcelService<GitHubRepository> excelService)
        {
            _emailService = emailService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _excelService = excelService;
        }

        [HttpPost("sendEmail/{emailAddress}")]
        [Authorize]
        public ActionResult SendEmail([FromRoute] string emailAddress, [FromBody] RepositoryToMailDto repo)
        {
            if (!Regex.Match(emailAddress, "^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$").Success)
                return BadRequest();

            _emailService.SendEmail(emailAddress, _mapper.Map<RepositoryToMailDto,GitHubRepository>(repo));
            return Ok();
        }


        [HttpPost]
        [Authorize]
        public async Task<ActionResult> BookmarkRepo([FromBody] GitHubRepositoryDto repoToCreate)
        {
            int? userId = GetIdFromToken();
            if (userId == null)
                return BadRequest();

            UserSpecification spec = new UserSpecification(userId.Value);

            AppUser user = await _unitOfWork.Repository<AppUser>().GetFirstOrDefaultBySpecAsync(spec);
            if (user == null || user.GitHubRepositories?.FirstOrDefault(x => x.GitHubId == repoToCreate.id) != null)
                return BadRequest();

            GitHubRepository repo = _mapper.Map<GitHubRepositoryDto, GitHubRepository>(repoToCreate);
            repo.User = user;
            repo.AvatarUrl = repo.owner.avatar_url;
            Owner owner = await _unitOfWork.Repository<Owner>().GetFirstOrDefaultBySpecAsync(new OwnerSpecification(repo.owner.Id));
            if (owner != null)
            {
                owner.Repositories.Add(repo);
                _unitOfWork.Repository<Owner>().Update(owner);
            }
            else
            {

                repo.owner.Repositories.Add(repo);
                _unitOfWork.Repository<Owner>().Add(repo.owner);
            }

            _unitOfWork.Complete();

            return Ok();
        }

        [HttpDelete("{gitHubId}")]
        [Authorize]
        public async Task<ActionResult> Unbookmark([FromRoute] int gitHubId)
        {
            int? userId = GetIdFromToken();
            if (userId == null)
                return BadRequest();

            GitHubRepositorySpecification spec = new GitHubRepositorySpecification(userId.Value, gitHubId);

            GitHubRepository repo = await _unitOfWork.Repository<GitHubRepository>().GetFirstOrDefaultBySpecAsync(spec);
            _unitOfWork.Repository<GitHubRepository>().Delete(repo);
            await _unitOfWork.Complete();
            return Ok();
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<GitHubRepository>>> Get()
        {
            int? userId = GetIdFromToken();
            if (userId == null)
                return BadRequest();

            UserSpecification spec = new UserSpecification(userId.Value);

            AppUser user = await _unitOfWork.Repository<AppUser>().GetFirstOrDefaultBySpecAsync(spec);
            if (user == null)
                return BadRequest();

            IReadOnlyList<RepositoryToTransferDto> repos = _mapper.Map<IReadOnlyList<GitHubRepository>, IReadOnlyList<RepositoryToTransferDto>>(user.GitHubRepositories as IReadOnlyList<GitHubRepository>);
            return Ok(repos);
        }


        [HttpGet("withPaging/size/{pageSize}/index/{pageIndex}")]
        [Authorize]
        public async Task<ActionResult<Pagination>> Get([FromRoute] int pageSize, [FromRoute] int pageIndex)
        {
            int? userId = GetIdFromToken();
            if (userId == null)
                return BadRequest();

            GitHubRepositorySpecification spec = new GitHubRepositorySpecification(userId.Value, pageSize, pageIndex);

            IReadOnlyList<GitHubRepository> repositories = await _unitOfWork.Repository<GitHubRepository>().ListBySpecAsync(spec);
            int totalRecords = await _unitOfWork.Repository<GitHubRepository>().CountAsync(new GitHubRepositorySpecification(userId.Value));


            IReadOnlyList<RepositoryToTransferDto> repos = _mapper.Map<IReadOnlyList<GitHubRepository>, IReadOnlyList<RepositoryToTransferDto>>(repositories);
            return Ok(new Pagination() { items = repos, total_count = totalRecords});
        }


        [HttpGet("exportToExcel")]
        [Authorize]
        public async Task<ActionResult> GetExcel()
        {
            int? userId = GetIdFromToken();
            if (userId == null)
                return BadRequest();

            GitHubRepositorySpecification spec = new GitHubRepositorySpecification(userId.Value);

            IReadOnlyList<GitHubRepository> repositories = await _unitOfWork.Repository<GitHubRepository>().ListBySpecAsync(spec);
            string fileName = _excelService.GetExcelFromObject(repositories);
            if (!string.IsNullOrEmpty(fileName))
            {

                return File(System.IO.File.ReadAllBytes(fileName), "application/vnd.ms-excel", Path.GetFileName(fileName));

               }
            return BadRequest();
        }
    }
}
