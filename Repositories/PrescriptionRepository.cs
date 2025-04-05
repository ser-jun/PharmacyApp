using PharmacyApp.Models;
using PharmacyApp.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApp.Repositories
{
    public class PrescriptionRepository : IPrescriptionrepository
    {
        private readonly ICrudRepository<Prescription> _crudPrescription;

        public PrescriptionRepository(PharmacyDbContext context)
        {
            _crudPrescription = new CrudRepository<Prescription>(context);
        }
        public async Task<IEnumerable<Prescription>> LoadPrescriptionInfo()
        {
            return await _crudPrescription.GetAllAsync();
        }
        public async Task AddPrescriptionItem(int customerId, int doctorId, DateTime issueDate, string diagnosis,
            string dosage, string duration)
        {
            var prescription = new Prescription
            {
                CustomerId = customerId,
                DoctorId = doctorId,
                IssueDate = issueDate,
                Diagnosis = diagnosis,
                Dosage = dosage,
                Duration = duration
            };
            await _crudPrescription.AddAsync(prescription);
        }
        public async Task DeletePrescritionItem(Prescription prescriptionToDelete)
        {
            await _crudPrescription.DeleteAsync(prescriptionToDelete);
        }
        public async Task UpdatePrescriptionItem(Prescription prescriptionToUpdate, int newCustomerId, int newDoctorId, DateTime newDate, 
            string newDiagnosis, string newDosage, string newDuration)
        {
            prescriptionToUpdate.CustomerId = newCustomerId;
            prescriptionToUpdate.DoctorId = newDoctorId;
            prescriptionToUpdate.IssueDate = newDate;
            prescriptionToUpdate.Diagnosis = newDiagnosis;
            prescriptionToUpdate.Dosage = newDosage;
            prescriptionToUpdate.Duration = newDuration;    
            await _crudPrescription.UpdateAsync(prescriptionToUpdate);
        }
    }
}
