using Core.Entities;

namespace GitHubExplorerApi.Specifications
{
    public class UserSpecification: BaseSpecification<AppUser>
    {
        public UserSpecification(string email) : base(x => x.Email == email)
        {

        }

        public UserSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.GitHubRepositories);
        }
    }
}
