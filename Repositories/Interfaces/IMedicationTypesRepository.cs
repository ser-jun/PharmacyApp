using PharmacyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApp.Repositories.Interfaces
{
    public interface IMedicationTypesRepository
    {
        Task<IEnumerable<MedicationType>> LoadTypesInfo();
        Task AddTypeItem(string name, string applicationMethod);
        Task DeleteTypeItem(MedicationType typeToDelete);
        Task UpdateTypeItem(MedicationType typeToUpdate, string name, string applicetionMethod);

    }
}
