using PharmacyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApp.Repositories.Interfaces
{
    public abstract class CrudAbstract<T> where T : class
    {
        private readonly PharmacyDbContext _context;
        public CrudAbstract(PharmacyDbContext context) 
        {
        _context = context;
        }   

    }
}
