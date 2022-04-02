using Core.Entities;

namespace GitHubExplorerApi.Specifications
{
    public class OwnerSpecification : BaseSpecification<Owner>
    {
        public OwnerSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.Repositories);
        }
    }
}
