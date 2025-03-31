using Microsoft.EntityFrameworkCore;
using PharmacyApp.Models;
using PharmacyApp.Repositories.Interfaces;
using System.Windows;

namespace PharmacyApp.Repositories
{
    public class UsersRepository : IUserRepository
    {
        private readonly PharmacyDbContext _context;
        private ICrudRepository<User> _userCrudRepository;
        public UsersRepository(PharmacyDbContext context)
        {
            _context = context;
            _userCrudRepository = new CrudRepository<User>(context);
        }

        public async Task<User> RegisterUser(string name, string password, string role, string fullName, string phone)
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
            return await _userCrudRepository.AddAsync(newUser);
        }
        public async Task<User> AuthenticateAsync(string name, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == name);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                return null;
            }

            return user;
        }
        public async Task DeleteUser(int userId)
        {
            var selectedUser = await _userCrudRepository.GetByIdAsync(userId);
            await _userCrudRepository.DeleteAsync(selectedUser);
        }
        public async Task UpdateUser(string newName, string newFullName, string newPhone, int userId)
        {
            var userForUpdating = await _userCrudRepository.GetByIdAsync(userId);

            userForUpdating = new User
            {
                Username = newName,
                PasswordHash = userForUpdating.PasswordHash,
                Role = userForUpdating.Role,
                FullName = newFullName,
                ContactPhone = newPhone
            };
            await _userCrudRepository.UpdateAsync(userForUpdating);
        }
    }
}
