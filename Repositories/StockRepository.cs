using PharmacyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PharmacyApp.Repositories.Interfaces;
using PharmacyApp.DTO;
using Microsoft.EntityFrameworkCore;


namespace PharmacyApp.Repositories
{
    public class StockRepository : IstockRepository
    {
        private readonly PharmacyDbContext _context;
        private  ICrudRepository<Models.Component> _crudComponent;
        private  ICrudRepository<Stock> _crudStock;
        public StockRepository(PharmacyDbContext context)
        {
            _context = context;
            _crudComponent = new CrudRepository<Models.Component>(context);
            _crudStock = new CrudRepository<Stock>(context);
        }
        public async Task<IEnumerable<StockItems>> LoadStockItems()
        {
            return await _context.Database.SqlQueryRaw<StockItems>("CALL GetStockWithComponents()").ToListAsync();
        }
        public async Task AddComponent(string componentName, string unitOfMesuare, decimal amount,
            decimal criticalNorm, DateTime arriveData, int shelfLife)
        {
            var component = new Models.Component
            {
                Name = componentName,
                UnitOfMeasure = unitOfMesuare,
                CriticalNorm = criticalNorm,
                ShelfLife = shelfLife
            };
            await _crudComponent.AddAsync(component);
            var stockItem = new Stock
            {
                ComponentId = component.ComponentId,
                Amount = amount,
                ArrivalDate = arriveData,
            };
            await _crudStock.AddAsync(stockItem);
        }
        public async Task DeleteItem(int ComponentId)
        {
            var componentToDelete= await _crudComponent.GetByIdAsync(ComponentId);
            await _crudComponent.DeleteAsync(componentToDelete);
        }
        public async Task UpdateItem(int componentId, int stockId, string newName, string newUnitMesuare, decimal newAmount,
    decimal newCriticalNorm, DateTime newArriveData, int newShelfLife)
        {
            var stockItem = await _crudStock.GetByIdAsync(stockId);
            var componentToUpdate = await _crudComponent.GetByIdAsync(componentId);

            componentToUpdate.Name = newName;
            componentToUpdate.UnitOfMeasure = newUnitMesuare;
            componentToUpdate.CriticalNorm = newCriticalNorm;
            componentToUpdate.ShelfLife = newShelfLife;

            await _crudComponent.UpdateAsync(componentToUpdate);
                stockItem.Amount = newAmount;
            stockItem.ArrivalDate = newArriveData;
            await _crudStock.UpdateAsync(stockItem);

        }

        public async Task<IEnumerable<StockItems>> GetFilteredDataByShelfLife()
        {
            return await _context.Database.SqlQueryRaw<StockItems>("CALL GetExpiringComponents()").ToListAsync();   
        }
    }
}
