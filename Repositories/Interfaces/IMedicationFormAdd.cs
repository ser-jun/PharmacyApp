using PharmacyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApp.Repositories.Interfaces
{
    public interface IMedicationFormAdd
    {
        Task AddMedicationItem(string medicationName, bool isreadyMade, decimal price, MedicationType type, MedicationCategory category);
        Task<IEnumerable<MedicationType>> LoadMedicationType();
        Task<IEnumerable<MedicationCategory>> LoadMedicationCategory();
    }
}
