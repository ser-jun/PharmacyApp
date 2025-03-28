using PharmacyApp.Models;
using PharmacyApp.Repositories.Interfaces;

namespace PharmacyApp.Repositories
{
    public class UsersRepository
    {
        private readonly PharmacyDbContext _context;
        private ICrudRepository<User> _userCrudRepository;
        public UsersRepository(PharmacyDbContext context)
        {
            _context = context;
            _userCrudRepository = new CrudRepository<User>(context);
        }
    }
}
