using PharmacyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApp.Repositories.Interfaces
{
    public interface IPrescriptionrepository
    {
        Task<IEnumerable<Prescription>> LoadPrescriptionInfo();
        Task AddPrescriptionItem(int customerId, int doctorId, DateTime issueDate, string diagnosis,
            string dosage, string duration);
        Task DeletePrescritionItem(Prescription prescriptionToDelete);
        Task UpdatePrescriptionItem(Prescription prescriptionToUpdate, int newCustomerId, int newDoctorId, DateTime newDate,
            string newDiagnosis, string newDosage, string newDuration);
    }
}
