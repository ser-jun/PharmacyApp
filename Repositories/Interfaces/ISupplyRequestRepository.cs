using PharmacyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApp.Repositories.Interfaces
{
    public interface ISupplyRequestRepository
    { 
        Task<IEnumerable<SupplyRequest>> LoadSupplyRequest();
        Task AddSupplyRequestItem(int componentId, decimal amount, DateTime? time, string status);
        Task DeleteSupplyRequestItem(SupplyRequest itemToDelete);
        Task UpdateSupplyRequestItem(SupplyRequest itemToUpdate, int newcomponentId, decimal newAmount, DateTime? newTime, string newStatus);
        //Task<IEnumerable<PendingOrder>> LoadComponentInfo();
    }
}
