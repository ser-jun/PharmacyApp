using Microsoft.EntityFrameworkCore;
using PharmacyApp.DTO;
using PharmacyApp.Models;
using PharmacyApp.Repositories.Interfaces;
namespace PharmacyApp.Repositories
{
    public class MedicationsRepository : IMedicationsRepository, IMedicationFormAdd
    {
        private readonly PharmacyDbContext _context;
        private readonly ICrudRepository<Medication> _crudOperationMedication;
        private readonly ICrudRepository<MedicationType> _crudTypeMedication;
        private readonly ICrudRepository<MedicationCategory> _crudCategoryMedication;
        public MedicationsRepository(PharmacyDbContext context)
        {
            _context = context;
            _crudOperationMedication = new CrudRepository<Medication>(context);
            _crudTypeMedication = new CrudRepository<MedicationType>(context);
            _crudCategoryMedication = new CrudRepository<MedicationCategory>(context);
        }
        public async Task<IEnumerable<MedicationsItems>> LoadMedicationsInfo()
        {
            return await _context.Database.SqlQueryRaw<MedicationsItems>("CALL GetFullMedicationInfo").ToListAsync();
        }
        public async Task AddMedicationItem(string medicationName, bool isreadyMade, decimal price, MedicationType type, MedicationCategory category)
        {
            var medication = new Medication
            {
                Name = medicationName,
                IsReadyMade = isreadyMade,
                Price = price,
                Type = type,
                Category = category
            };
            await _crudOperationMedication.AddAsync(medication);
        }
        public async Task DeleteMedicationItem(int medicationId)
        {
            var itemToDelete = await _crudOperationMedication.GetByIdAsync(medicationId);
            await _crudOperationMedication.DeleteAsync(itemToDelete);
        }
        public async Task UpdateMedicationItem(int medicationId, string newName, bool newStatus, decimal newPrice, MedicationType newType, MedicationCategory newCategory)
        {
            var medication = await _crudOperationMedication.GetByIdAsync(medicationId);

            medication.Name = newName;
            medication.IsReadyMade = newStatus;
            medication.Price = newPrice;
            medication.Type = newType;
            medication.Category = newCategory;

            await _crudOperationMedication.UpdateAsync(medication);
        }
        public async Task<IEnumerable<MedicationType>> LoadMedicationType()
        {
            return await _crudTypeMedication.GetAllAsync();
        }
        public async Task<IEnumerable<MedicationCategory>> LoadMedicationCategory()
        {
            return await _crudCategoryMedication.GetAllAsync();
        }
    }
}
