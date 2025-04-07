using PharmacyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApp.Services
{
    public interface ICurrentUserService
    {
        User CurrentUser { get; }
        bool IsAuthenticated { get; }
        bool IsAdmin { get; }
        bool IsPharmacist { get; }
        bool IsCustomer { get; }
        void Login(User user);
        void Logout();
        bool HasRole(string role);
        bool HasAnyRole(params string[] roles);
    }
}
