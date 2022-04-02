
namespace GitHubExplorerApi.Specifications
{
    public class LicenseSpecification: BaseSpecification<Core.Entities.License>
    {
        public LicenseSpecification(string node_id) : base(x => x.node_id == node_id)
        {
            AddInclude(x => x.Repositories);
        }
    }
}
