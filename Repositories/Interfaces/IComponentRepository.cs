using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PharmacyApp.Models;

namespace PharmacyApp.Repositories.Interfaces
{
    public interface IComponentRepository
    {
        Task<IEnumerable<Models.Component>> GetComponentList();
        Task AddComponentItem(string name, string unitOfMesuare, decimal criticalNorm, int shelfLife);
        Task DeleteComponentItem(Models.Component itemToDelete);
        Task UpdateComponentItem(Models.Component itemToUpdate, string newName, string newUnitMesuare, decimal newCriticalNorm, int newShelfLife);
    }
}
