using PharmacyApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PharmacyApp.Repositories.Interfaces;

namespace PharmacyApp.Repositories
{
    public class ComponentRepository : IComponentRepository
    {
        private readonly PharmacyDbContext _context;
        private readonly ICrudRepository<Models.Component> _crudComponent;
        public ComponentRepository(PharmacyDbContext context) 
        {
        _context = context;
            _crudComponent = new CrudRepository<Models.Component>(context);

        }
        public async Task<IEnumerable<Models.Component>> GetComponentList()
        {
            return (await _crudComponent.GetAllAsync()).ToList();
        }
        public async Task AddComponentItem(string name, string unitOfMesuare, decimal criticalNorm, int shelfLife)
        {
            var component = new Models.Component
            {
                Name = name,
                UnitOfMeasure = unitOfMesuare,
                CriticalNorm = criticalNorm,
                ShelfLife = shelfLife
            };
            await _crudComponent.AddAsync(component);
        }
        public async Task DeleteComponentItem(Models.Component itemToDelete)
        {
            await _crudComponent.DeleteAsync(itemToDelete);
        }
        public async Task UpdateComponentItem(Models.Component itemToUpdate, string newName, string newUnitMesuare, decimal newCriticalNorm, int newShelfLife)
        {
            itemToUpdate.Name = newName;
            itemToUpdate.UnitOfMeasure = newUnitMesuare;
            itemToUpdate.CriticalNorm = newCriticalNorm;
            itemToUpdate.ShelfLife = newShelfLife;
            await _crudComponent.UpdateAsync(itemToUpdate);
        }
    }
}
