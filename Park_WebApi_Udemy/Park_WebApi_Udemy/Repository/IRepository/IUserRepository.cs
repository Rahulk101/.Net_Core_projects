using Park_WebApi_Udemy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Park_WebApi_Udemy.Repository.IRepository
{
    public interface IUserRepository
    {
        bool IsUniqueUser(string username);
        User Authenticate(string username, string password);
        User Register(string username, string password);
    }
}
