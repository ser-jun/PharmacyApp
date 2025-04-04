using Microsoft.EntityFrameworkCore;
using PharmacyApp.DTO;
using PharmacyApp.Models;
using PharmacyApp.Repositories.Interfaces;

namespace PharmacyApp.Repositories
{
    public class SupplierRepository
    {
        private readonly ICrudRepository<Supplier> _crudSupplier;
        private readonly ICrudRepository<SupplierComponent> _crudSupplierComponent;
        private readonly PharmacyDbContext _context;
        public SupplierRepository (PharmacyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SupplierDto>> LoadSupplierInfo()
        {
            return await _context.Database.SqlQueryRaw<SupplierDto>("CALL GetSupplier()").ToListAsync();
        }
        public async Task AddSupplier(string name, string? contactPerson, string phone, string email,
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
                    ComponentId = component.ComponentId,
                    DeliveryTime = deliveryTime,
                    UnitPrice = price
                };
                await _crudSupplierComponent.AddAsync(supplierComponent);
            }
        }
    }
}
 