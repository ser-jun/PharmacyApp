using PharmacyApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PharmacyApp.Models;

namespace PharmacyApp.Repositories.Interfaces
{
   public interface ISupplierRepository
    {
        Task<IEnumerable<SupplierDto>> LoadSupplierInfo();
        Task AddSupplierItem(string name, string? contactPerson, string phone, string email,
            sbyte raiting, int? deliveryTime, decimal price, List<Component> supplierComponents);
        Task DeleteSupplierItem(int supplierId);
        Task UpdateSupplierItem(int supplierId, string name, string? contactPerson, string phone, string email, sbyte rating,
        int? deliveryTime, decimal price, List<Component> supplierComponents);
    }
}
