using Management.Common.Models.Entity;
using Microsoft.AspNetCore.Identity;

namespace Management.Common.Models
{
    public class User : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public List<UserProject> Projects { get; set; } 
    }
}