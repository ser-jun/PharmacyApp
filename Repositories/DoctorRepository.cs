using PharmacyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PharmacyApp.Repositories;
using PharmacyApp.Repositories.Interfaces;

namespace PharmacyApp.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly PharmacyDbContext _context;
        private readonly ICrudRepository<Doctor> _crudDoctor;
        public DoctorRepository(PharmacyDbContext context) 
        {
        _context = context;
            _crudDoctor = new CrudRepository<Doctor>(context);
        }
        public async Task<IEnumerable<Doctor>> LoadDoctorsInfo()
        {
            return await _crudDoctor.GetAllAsync();
        }
        public async Task AddDoctorItem(string fullName, string license, string contactInfo)
        {
            var doctor = new Doctor
            {
                FullName = fullName,
                LicenseNumber = license,
                ContactInfo = contactInfo
            };
            await _crudDoctor.AddAsync(doctor);
        }
        public async Task DeleteDoctorItem(Doctor doctorToDelete)
        {
            await _crudDoctor.DeleteAsync(doctorToDelete);
        }
        public async Task UpdateDoctorItem(Doctor doctorToUpdate, string newFullName, string newLicense, string newContactInfo)
        {
            doctorToUpdate.FullName = newFullName;
            doctorToUpdate.LicenseNumber = newLicense;
            doctorToUpdate.ContactInfo = newContactInfo;
            await _crudDoctor.UpdateAsync(doctorToUpdate);
        }
        

    }
}
