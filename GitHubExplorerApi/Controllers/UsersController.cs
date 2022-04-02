using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using GitHubExplorerApi.Dtos;
using GitHubExplorerApi.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GitHubExplorerApi.Controllers
{

    public class UsersController : BaseController
    {

        ITokenService _tokenService;
        IUnitOfWork _unitOfWork;
        IMapper _mapper;
        IHashingService _hashingService;
        public UsersController(IUnitOfWork unitOfWork, ITokenService tokenService, IMapper mapper, IHashingService hashingService)
        {
            _tokenService = tokenService;
            _unitOfWork= unitOfWork;
            _mapper=mapper;
            _hashingService = hashingService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserToReturnDto>> Post(RegisterDto userToCreate)
        {
            
            AppUser usersWithSameEmail = await _unitOfWork.Repository<AppUser>().GetFirstOrDefaultBySpecAsync(new UserSpecification(userToCreate.Email));
            
            if (usersWithSameEmail != null) return BadRequest();

            AppUser user = _mapper.Map<RegisterDto, AppUser>(userToCreate);
                        
            _hashingService.HashPassword(userToCreate.Password, out byte[] hash, out byte[] salt);
            user.PasswordHash = hash;
            user.PasswordSalt = salt;
            _unitOfWork.Repository<AppUser>().Add(user);
            await _unitOfWork.Complete();

            return Ok(new UserToReturnDto() { Token = _tokenService.CreateToken(user) , Email = userToCreate.Email, DisplayName = userToCreate.DisplayName});
        }


        [HttpPost("login")]
        public async Task<ActionResult<UserToReturnDto>> Login(LoginDto  loginUser)
        {

            AppUser usersWithSameEmail = await _unitOfWork.Repository<AppUser>().GetFirstOrDefaultBySpecAsync(new UserSpecification(loginUser.Email));

            if (usersWithSameEmail == null) return BadRequest();

            if(_hashingService.CheckHashEquality(loginUser.Password, usersWithSameEmail.PasswordHash, usersWithSameEmail.PasswordSalt))
                return Ok(new UserToReturnDto() { Token = _tokenService.CreateToken(usersWithSameEmail), Email = usersWithSameEmail.Email, DisplayName = usersWithSameEmail.DisplayName });
            return Unauthorized();
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserToReturnDto>> Get()
        {
            int? accountid = GetIdFromToken();
            if (accountid == null)
                return Unauthorized();


            AppUser usersWithSameEmail = await _unitOfWork.Repository<AppUser>().GetByIdAsync(accountid.Value);

            if (usersWithSameEmail == null) return BadRequest();
                return Ok(new UserToReturnDto() { Token = _tokenService.CreateToken(usersWithSameEmail), Email = usersWithSameEmail.Email, DisplayName = usersWithSameEmail.DisplayName });
            
            return Unauthorized();
        }
    }
}