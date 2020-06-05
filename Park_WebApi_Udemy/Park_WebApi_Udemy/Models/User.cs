using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Park_WebApi_Udemy.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        [NotMapped]//This will not add the token property to the database.
        public string Token { get; set; }//will be generated to authenticate the user.
    }
}
