using System.ComponentModel.DataAnnotations;

namespace GitHubExplorerApi.Dtos
{
    public class RepositoryToMailDto
    {
        public string? AvatarUrl { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
