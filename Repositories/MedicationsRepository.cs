using Microsoft.EntityFrameworkCore;
using PharmacyApp.DTO;
using PharmacyApp.Models;
using PharmacyApp.Repositories.Interfaces;
namespace PharmacyApp.Repositories
{
    public class MedicationsRepository : IMedicationsRepository, IMedicationFormAddEdit
    {
        private readonly PharmacyDbContext _context;
        private readonly ICrudRepository<Medication> _crudOperationMedication;
        private readonly ICrudRepository<MedicationType> _crudTypeMedication;
        private readonly ICrudRepository<MedicationCategory> _crudCategoryMedication;
        private readonly ICrudRepository<Component> _crudComponent;
        private readonly ICrudRepository<MedicationComponent> _crudMedicationComponent;
        public MedicationsRepository(PharmacyDbContext context)
        {
            _context = context;
            _crudOperationMedication = new CrudRepository<Medication>(context);
            _crudTypeMedication = new CrudRepository<MedicationType>(context);
            _crudCategoryMedication = new CrudRepository<MedicationCategory>(context);
            _crudMedicationComponent = new CrudRepository<MedicationComponent>(context);    
            _crudComponent = new CrudRepository<Component>(context);
        }
        public async Task<IEnumerable<MedicationsItems>> LoadMedicationsInfo()
        {
            return await _context.Database.SqlQueryRaw<MedicationsItems>("CALL GetFullMedicationInfo").ToListAsync();
        }
        public async Task AddMedicationItem(string medicationName, bool isReadyMade, decimal price,
     MedicationType type, MedicationCategory category,
     List<Component> components, Dictionary<int, decimal> componentsAmount)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {

                var medication = new Medication
                {
                    Name = medicationName,
                    IsReadyMade = isReadyMade,
                    Price = price,
                    Type = type,
                    Category = category
                };

                await _crudOperationMedication.AddAsync(medication);

                foreach (var component in components)
                {
                    var medicationComponent = new MedicationComponent
                    {
                        MedicationId = medication.MedicationId,
                        ComponentId = component.ComponentId,
                        Amount = componentsAmount[component.ComponentId]
                    };
                    await _crudMedicationComponent.AddAsync(medicationComponent);
                }
                await transaction.CommitAsync();
            }

        }
        public async Task<IEnumerable<Component>> GetComponentList()
        {
            return await _crudComponent.GetAllAsync();
        }
        public async Task DeleteMedicationItem(int medicationId)
        {
            var itemToDelete = await _crudOperationMedication.GetByIdAsync(medicationId);
            await _crudOperationMedication.DeleteAsync(itemToDelete);
        }
        public async Task UpdateMedicationItem(int medicationId, string newName, bool newStatus, decimal newPrice, 
            MedicationType newType, MedicationCategory newCategory, List<Component> newComponents,
            Dictionary<int, decimal> newComponentsAmount)
        {
            var medication = await _crudOperationMedication.GetByIdAsync(medicationId);

            medication.Name = newName;
            medication.IsReadyMade = newStatus;
            medication.Price = newPrice;
            medication.Type = newType;
            medication.Category = newCategory;

            await _crudOperationMedication.UpdateAsync(medication);
            var existingComponents = _context.MedicationComponents
                  .Where(mc => mc.MedicationId == medicationId)
                  .ToList();

            foreach (var component in existingComponents)
            {
                await _crudMedicationComponent.DeleteAsync(component);
            }

            foreach (var component in newComponents)
            {
                var medicationComponent = new MedicationComponent
                {
                    MedicationId = medicationId,
                    ComponentId = component.ComponentId,
                    Amount = newComponentsAmount[component.ComponentId]
                };
                await _crudMedicationComponent.AddAsync(medicationComponent);
            }
        }
        public async Task<IEnumerable<MedicationType>> LoadMedicationType()
        {
            return await _crudTypeMedication.GetAllAsync();
        }
        public async Task<IEnumerable<MedicationCategory>> LoadMedicationCategory()
        {
            return await _crudCategoryMedication.GetAllAsync();
        }
        public async Task<IEnumerable<MedicationsItems>> SearchMedicationByName(string name)
        {
            return await _context.Database.SqlQueryRaw<MedicationsItems>("CALL SearchMedications({0})", name).ToListAsync();
        }
    }
}
