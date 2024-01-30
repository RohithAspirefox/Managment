using System.ComponentModel.DataAnnotations;

namespace Management.Common.Models.DTO
{
    public class LoginModelDto
    {
        [Required(ErrorMessage = "Email iss required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}