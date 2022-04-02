using System.ComponentModel.DataAnnotations;

namespace GitHubExplorerApi.Dtos
{
    public class LoginDto
    {
        [Required]
        [RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }


    }
}
