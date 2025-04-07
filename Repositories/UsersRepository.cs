using Microsoft.EntityFrameworkCore;
using PharmacyApp.Models;
using PharmacyApp.Repositories.Interfaces;
using System.Data;
using System.Windows;

namespace PharmacyApp.Repositories
{
    public class UsersRepository : IUserRepository, IAdminRepository
    {
        private readonly PharmacyDbContext _context;
        private ICrudRepository<User> _userCrudRepository;
        private readonly ICrudRepository<Customer> _crudCustomer;
        public UsersRepository(PharmacyDbContext context)
        {
            _context = context;
            _userCrudRepository = new CrudRepository<User>(context);
            _crudCustomer = new CrudRepository<Customer>(context);
        }

        public async Task<User> RegisterUser(string name, string password, string role, string fullName, string phone, DateTime birth, string address)
        {
            if (await _context.Users.AnyAsync(u => u.Username == name))
            {
                MessageBox.Show("Пользователь с таким именем уже существует");
            }
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
            var newUser = new User
            {
                Username = name,
                PasswordHash = passwordHash,
                Role = role,
                FullName = fullName,
                ContactPhone = phone
            };
            if (role == "customer")
            {
                var newCustomer = new Customer
                {
                    FullName = fullName,
                    BirthDate = birth,
                    Address = address
                };
                await _crudCustomer.AddAsync(newCustomer);
            }
            return await _userCrudRepository.AddAsync(newUser);
        }
        public async Task<User> AuthenticateAsync(string name, string password)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Имя пользователя и пароль обязательны для заполнения");
                return null;
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == name);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                MessageBox.Show("Неверное имя пользователя или пароль");
                return null;
            }

            return user;
        }
        public async Task DeleteItemUser(int userId)
        {
            var selectedUser = await _userCrudRepository.GetByIdAsync(userId);
            await _userCrudRepository.DeleteAsync(selectedUser);
        }
        public async Task UpdateItemUser(string newName, string newFullName, string newPhone, int userId)
        {
            var userForUpdating = await _userCrudRepository.GetByIdAsync(userId);

            userForUpdating.Username = newName;
            userForUpdating.FullName= newFullName;
            userForUpdating.ContactPhone = newPhone;    
            await _userCrudRepository.UpdateAsync(userForUpdating);
        }
        public async Task<IEnumerable<User>> LoadUsers()
        {
            return await _userCrudRepository.GetAllAsync(); 
        }
    }
}
