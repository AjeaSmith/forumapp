using System;
using System.ComponentModel.DataAnnotations;

namespace Forum.Models.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "This field is required")]
        [StringLength(12, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [Required(ErrorMessage = "This field is required")]
        public string Password { get; set; }

        public bool success { get; set; }
    }
}
