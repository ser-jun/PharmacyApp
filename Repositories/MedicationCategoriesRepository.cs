using PharmacyApp.Models;
using PharmacyApp.Repositories.Interfaces;

namespace PharmacyApp.Repositories
{
    public class MedicationCategoriesRepository : IMedicationCategoryRepository
    {
        private readonly PharmacyDbContext _context;
        private ICrudRepository<MedicationCategory> _crudCategories;
        public MedicationCategoriesRepository(PharmacyDbContext context)
        {
            _context = context;
            _crudCategories = new CrudRepository<MedicationCategory>(context);
        }

        public async Task<IEnumerable<MedicationCategory>> LoadCategoriesInfo()
        {
            return await _crudCategories.GetAllAsync();
        }
        public async Task AddCategoryItem(string name, string description)
        {
            var category = new MedicationCategory
            { 
                Name= name,
                Description= description
            };
            await _crudCategories.AddAsync(category);
        }
        public async Task DeleteCategoryItem(MedicationCategory category)
        {
            await _crudCategories.DeleteAsync(category);
        }
        public async Task UpdateCategoryItem(MedicationCategory categoryToUpdate, string newName, string newDescription)
        {
            categoryToUpdate.Name = newName;
            categoryToUpdate.Description = newDescription;
            await _crudCategories.UpdateAsync(categoryToUpdate);
        }
    }
}
