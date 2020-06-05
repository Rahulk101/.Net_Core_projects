using Microsoft.AspNetCore.Identity;

namespace Venkat.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string City { get; set; }
    }
}