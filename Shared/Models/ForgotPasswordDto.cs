using System.ComponentModel.DataAnnotations;

namespace Management.Common.Models
{
    public class ForgotPasswordDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}