using PharmacyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApp.Repositories.Interfaces
{
    public interface IDoctorRepository
    {
        Task<IEnumerable<Doctor>> LoadDoctorsInfo();
        Task AddDoctorItem(string fullName, string license, string contactInfo);
        Task DeleteDoctorItem(Doctor doctorToDelete);
        Task UpdateDoctorItem(Doctor doctorToUpdate, string newFullName, string newLicense, string newContactInfo);
    }
}
