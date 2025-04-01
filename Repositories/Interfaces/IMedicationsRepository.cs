using PharmacyApp.DTO;
using PharmacyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApp.Repositories.Interfaces
{
    public interface IMedicationsRepository
    {
        Task<IEnumerable<MedicationsItems>> LoadMedicationsInfo();
        Task DeleteMedicationItem(int medicationId);
        Task UpdateMedicationItem(int medicationId, string newName, bool newStatus, decimal newPrice, MedicationType newType, MedicationCategory newCategory);
    }
}
