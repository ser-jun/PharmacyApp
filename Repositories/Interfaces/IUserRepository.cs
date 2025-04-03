using PharmacyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApp.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> RegisterUser(string name, string password, string role, string fullName, string phone, DateTime birth, string address);
        Task<User> AuthenticateAsync(string name, string password);  
        }
}
