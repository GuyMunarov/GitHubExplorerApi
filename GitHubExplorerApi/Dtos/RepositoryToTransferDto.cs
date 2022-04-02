using System.ComponentModel.DataAnnotations;

namespace GitHubExplorerApi.Dtos
{
    public class RepositoryToTransferDto
    {
        public int id { get; set; }
        [Required]
        public string avatar_url { get; set; }
        [Required]
        public string name { get; set; }
        public string? description { get; set; }
    }
}
