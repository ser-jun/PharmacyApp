using PharmacyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApp.Repositories.Interfaces
{
    public interface IAdminRepository
    {
        Task UpdateItemUser(string newName, string newFullName, string newPhone, int userId);
        Task DeleteItemUser(int userId);
        Task<IEnumerable<User>> LoadUsers();
    }
}
