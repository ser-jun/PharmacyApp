using PharmacyApp.Models;
using PharmacyApp.Repositories.Interfaces;

namespace PharmacyApp.Repositories
{
    public class MedicationTypesRepository : IMedicationTypesRepository
    {

        private readonly ICrudRepository<MedicationType> _crudType;
        public MedicationTypesRepository(PharmacyDbContext context)
        {
            _crudType = new CrudRepository<MedicationType>(context);
        }
        public async Task<IEnumerable<MedicationType>> LoadTypesInfo()
        {
            return await _crudType.GetAllAsync();
        }
        public async Task AddTypeItem(string name, string applicationMethod)
        {
            var type = new MedicationType()
            {
                Name = name,
                ApplicationMethod = applicationMethod
            };
            await _crudType.AddAsync(type);
        }
        public async Task DeleteTypeItem(MedicationType typeToDelete)
        {
            await _crudType.DeleteAsync(typeToDelete);
        }
        public async Task UpdateTypeItem(MedicationType typeToUpdate, string name, string applicetionMethod)
        {
            typeToUpdate.Name = name;
            typeToUpdate.ApplicationMethod = applicetionMethod;
            await _crudType.UpdateAsync(typeToUpdate);
        }
    }
}
