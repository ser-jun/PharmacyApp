using PharmacyApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApp.Repositories.Interfaces
{
    public interface IstockRepository
    {
        Task<IEnumerable<StockItems>> LoadStockItems();
        Task AddComponent(string componentName, string unitOfMesuare, decimal amount,
            decimal criticalNorm, DateTime arriveData, int shelfLife);
        Task DeleteItem(int ComponentId);
        Task UpdateItem(int componentId, int stockId, string newName, string newUnitMesuare, decimal newAmount,
    decimal newCriticalNorm, DateTime newArriveData, int newShelfLife);
    }
}
