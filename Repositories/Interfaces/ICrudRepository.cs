using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApp.Repositories.Interfaces
{
    public interface ICrudRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int idEntityForSearch);
        Task<T> AddAsync(T entity);
        Task DeleteAsync(T entityForDelete);
        Task<T> UpdateAsync(T newEntity);
    }
}
