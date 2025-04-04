using Microsoft.EntityFrameworkCore;
using PharmacyApp.DTO;
using PharmacyApp.Models;
using PharmacyApp.Repositories.Interfaces;

namespace PharmacyApp.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly ICrudRepository<Supplier> _crudSupplier;
        private readonly ICrudRepository<SupplierComponent> _crudSupplierComponent;
        private readonly PharmacyDbContext _context;
        public SupplierRepository (PharmacyDbContext context)
        {
            _context = context;
            _crudSupplier = new CrudRepository<Supplier>(context);
            _crudSupplierComponent = new CrudRepository<SupplierComponent>(context);    
        }

        public async Task<IEnumerable<SupplierDto>> LoadSupplierInfo()
        {
            return await _context.Database.SqlQueryRaw<SupplierDto>("CALL GetSupplier()").ToListAsync();
        }
        public async Task AddSupplierItem(string name, string? contactPerson, string phone, string email,
            sbyte raiting, int? deliveryTime, decimal price, List<Component> supplierComponents)
        {
            var supplier = new Supplier
            {
                Name = name,
                ContactPerson = contactPerson,
                Phone = phone,
                Email = email,
                Rating = raiting
            };
            await _crudSupplier.AddAsync(supplier);
            foreach (var component in supplierComponents)
            {
                var supplierComponent = new SupplierComponent
                {
                    SupplierId =supplier.SupplierId,
                    ComponentId = component.ComponentId,
                    DeliveryTime = deliveryTime,
                    UnitPrice = price
                };
                await _crudSupplierComponent.AddAsync(supplierComponent);
            }
        }
        public async Task DeleteSupplierItem(int supplierId)
        {
            var dataToDelete = await _crudSupplier.GetByIdAsync(supplierId);
            await _crudSupplier.DeleteAsync(dataToDelete);
        }
        public async Task UpdateSupplierItem(int supplierId, string name, string? contactPerson, string phone, string email,sbyte rating,
        int? deliveryTime, decimal price, List<Component> supplierComponents)
        {
            var supplier = await _crudSupplier.GetByIdAsync(supplierId);
            supplier.Name = name;
            supplier.ContactPerson = contactPerson;
            supplier.Phone = phone;
            supplier.Email = email;
            supplier.Rating = rating;

            await _crudSupplier.UpdateAsync(supplier);

            var existingComponents = await _crudSupplierComponent.GetAllAsync();

            foreach (var existingComponent in existingComponents)
            {
                await _crudSupplierComponent.DeleteAsync(existingComponent);
            }

            foreach (var component in supplierComponents)
            {
                var supplierComponent = new SupplierComponent
                {
                    SupplierId = supplierId, 
                    ComponentId = component.ComponentId,
                    DeliveryTime = deliveryTime,
                    UnitPrice = price
                };
                await _crudSupplierComponent.AddAsync(supplierComponent);
            }
        }
    }
}
 