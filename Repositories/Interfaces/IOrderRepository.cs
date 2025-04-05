using PharmacyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApp.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> LoadOrdersInfo();
        Task AddOrderItem(int prescriptionId, int medicationId, decimal amount, int registarId, DateTime? orderDate,
            string status, decimal? price);
        Task DeleteOrderItem(Order orderToDelete);
        Task UpdateOrderItem(Order orderToUpdate, int newPrescriptionId, int newMedicationId, decimal newAmount, int newRegistratId,
            DateTime? newOrderDate, string newStatus, decimal? newPrice);
    }
}
