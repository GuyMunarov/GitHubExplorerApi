using Core.Entities;

namespace GitHubExplorerApi.Specifications
{
    public class GitHubRepositorySpecification : BaseSpecification<GitHubRepository>
    {
        public GitHubRepositorySpecification(int userId, int gitHubId) : base(x => x.UserId == userId && x.GitHubId == gitHubId)
        {

        }
        public GitHubRepositorySpecification(int userId) : base(x => x.UserId == userId)
        {

        }
        public GitHubRepositorySpecification(int userId, int PageSize, int PageIndex) : base(x => x.UserId == userId)
        {
            ApplyPaging(PageSize * (PageIndex - 1), PageSize);
        }



    }
}

