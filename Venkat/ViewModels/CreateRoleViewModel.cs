using System.ComponentModel.DataAnnotations;

namespace Venkat.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}