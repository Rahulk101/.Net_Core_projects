using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Venkat.Models.Utilities;

namespace Venkat.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Remote("IsEmailInUse","account")]
        [ValidEmailDomainAttribute(allowedDomain:"gmail.com",
                    ErrorMessage="Email domain must be gmail.com")]
        public string Email { get; set; }

        [Required]
        // [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password",
            ErrorMessage = "Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string City { get; set; }
    }
}