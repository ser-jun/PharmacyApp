using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApp.Repositories.Interfaces
{
    public interface IAdminRepository
    {
        Task UpdateUser(string newName, string newFullName, string newPhone, int userId);
        Task DeleteUser(int userId);
    }
}
