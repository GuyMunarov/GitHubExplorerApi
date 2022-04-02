using System.ComponentModel.DataAnnotations;

namespace GitHubExplorerApi.Dtos
{
    public class RegisterDto
    {
        [Required]
        public string DisplayName { get; set; }
        [Required]
        [RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$")]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }


    }
}
