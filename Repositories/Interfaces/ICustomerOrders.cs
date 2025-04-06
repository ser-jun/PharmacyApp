using PharmacyApp.DTO;
using PharmacyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApp.Repositories.Interfaces
{
    public interface ICustomerOrders
    {
        Task<IEnumerable<UnclaimedOrdersDto>> LoadUnclamedOrders();
   
        Task<IEnumerable<UnclaimedOrdersDto>> LoadWaitingMedicationInfo(int? categoryId = null);
        Task<IEnumerable<MedicationCategory>> LoadMedicationCategory();
    }
}
