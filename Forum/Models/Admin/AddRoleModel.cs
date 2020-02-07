using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Forum.ForumData.Models;

namespace Forum.Models.Admin
{
    public class AddRoleModel
    {
        [Required]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }

        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [EmailAddress]
        [Required]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public IEnumerable<ApplicationUser> AdminUsers { get; set; }

    }
}
