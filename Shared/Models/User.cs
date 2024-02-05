using Microsoft.AspNetCore.Identity;

namespace Management.Common.Models
{
    public class User : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ImageURL { get; set; }
        public string? DocumentURL { get; set; }
        public bool? IsLogged { get; set; } = false;

        public DateTime? DateOfJoining { get; set; }

        public string? Gender { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }
        public string? Active { get; set; } = "Yes";

    }
}