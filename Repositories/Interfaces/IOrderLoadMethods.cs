using PharmacyApp.DTO;
using PharmacyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApp.Repositories.Interfaces
{
    public interface IOrderLoadMethods
    {
        Task<IEnumerable<Prescription>> LoadPrescriptionInfo();
        Task<IEnumerable<Medication>> LoadMedicatonInfo();
        Task<IEnumerable<User>> LoadUserInfo();
        Task<IEnumerable<Models.Component>> LoadComponentInfo();
        Task<IEnumerable<Order>> LoadOrderInProcess();
    }
}
