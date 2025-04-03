using PharmacyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApp.Repositories.Interfaces
{
    public interface IMedicationCategoryRepository
    {
        Task<IEnumerable<MedicationCategory>> LoadCategoriesInfo();
        Task AddCategoryItem(string name, string description);
        Task DeleteCategoryItem(MedicationCategory category);
        Task UpdateCategoryItem(MedicationCategory categoryToUpdate, string newName, string newDescription);

    }
}
