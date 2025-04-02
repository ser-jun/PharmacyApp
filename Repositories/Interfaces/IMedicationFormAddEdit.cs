using PharmacyApp.DTO;
using PharmacyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApp.Repositories.Interfaces
{
    public interface IMedicationFormAddEdit
    {
        Task AddMedicationItem(string medicationName, bool isReadyMade, decimal price,
       MedicationType type, MedicationCategory category,
       List<Component> components, Dictionary<int, decimal> componentsAmount);
        Task UpdateMedicationItem(int medicationId, string newName, bool newStatus, decimal newPrice, MedicationType newType, MedicationCategory newCategory);
        Task<IEnumerable<MedicationType>> LoadMedicationType();
        Task<IEnumerable<MedicationCategory>> LoadMedicationCategory();
        Task<IEnumerable<Component>> GetComponentList();
    }
}
