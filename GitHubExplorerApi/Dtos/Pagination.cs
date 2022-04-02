namespace GitHubExplorerApi.Dtos
{
    public class Pagination
    {
        public int total_count { get; set; }
        public IReadOnlyList<object> items { get; set; }
        
    }
}
