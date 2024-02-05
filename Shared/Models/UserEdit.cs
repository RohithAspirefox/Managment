using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Management.Common.Models
{
    public class UserEdit
    {
        
        public Guid Id { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Enter First Name")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please Enter Last  Name")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please Enter Mobile  No")]
        [Display(Name = "Mobile")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "Please Enter Your Address")]
        [Display(Name = "Address")]
        public string Address { get; set; }


        [Display(Name = "City")]
        public string City { get; set; }

      
        [Required(ErrorMessage = "Please choose profile image")]
        [Display(Name = "Profile Picture")]
        public IFormFile ProfileImage { get; set; }

        [Required(ErrorMessage = "Please Enter Date Of Joining")]
        [Display(Name = "DateOfJoining")]
        public DateTime DateOfJoining { get; set; }




    }
}
