using PharmacyApp.Models;
using PharmacyApp.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PharmacyApp.DTO;
using Microsoft.EntityFrameworkCore;
namespace PharmacyApp.Repositories
{
    public class MedicationsRepository
    {
        private readonly PharmacyDbContext _context;
        
        public MedicationsRepository(PharmacyDbContext context) 
        {
        _context = context;
        }

        public async Task<IEnumerable<MedicationsItems>> LoadMedicationsInfo()
        {
            return await _context.Database.SqlQueryRaw<MedicationsItems>("CALL GetFullMedicationInfo").ToListAsync();
        }
    }
}
