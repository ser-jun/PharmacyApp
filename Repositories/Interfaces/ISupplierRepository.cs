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
            sbyte raiting, int? deliveryTime, List<Component> supplierComponents);
        Task DeleteSupplierItem(int supplierId);
        Task UpdateSupplierItem(int supplierId, string name, string? contactPerson, string phone, string email, sbyte rating,
        int? deliveryTime, List<Component> supplierComponents);
        Task<IEnumerable<SupplierDto>> FilterByRatingOrComponent(List<int> components, byte? rating);
        Task<IEnumerable<SupplierDto>> SearchByName(string name);
    }
}
