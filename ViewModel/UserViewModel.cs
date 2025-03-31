using Microsoft.EntityFrameworkCore.ChangeTracking;
using PharmacyApp.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PharmacyApp.ViewModel
{
    public class UserViewModel : INotifyPropertyChanged
    {

        public ICommand EntryCommand { get; }
        public ICommand RegisterCommand { get; }

        private string _userName;
        private string _password;
        private string _phoneNumber;
        private string _fullName;
        private string _selectedRole;


        private readonly IUserRepository _userRepository;
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<string> DisplayRoles { get; } = new ObservableCollection<string>
        {
            "Администратор",
            "Фармацевт",
            "Пользователь"
        };

        private readonly Dictionary<string, string> _roleMap = new Dictionary<string, string>
        {
            ["Администратор"] = "admin",
            ["Фармацевт"] = "pharmacist",
            ["Пользователь"] = "customer"
        };
        public UserViewModel(IUserRepository userRepository) 
        {
            _userRepository = userRepository;
            RegisterCommand = (ICommand)new RelayCommand(async () => await ExecuteRegister());
            EntryCommand = new RelayCommand(async () => await ExecuteEntry());
        }
  
        public string SelectedRole
        {
            get => _selectedRole;
            set
            {
                _selectedRole = value;
                OnPropertyChanged(nameof(SelectedRole));
            }
        }
        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public string FullName
        {
            get => _fullName;
            set
            {
                _fullName = value;
                OnPropertyChanged(nameof(FullName));
            }
        }

        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                _phoneNumber = value;
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }


        private async Task ExecuteEntry()
        {
            var user = await _userRepository.AuthenticateAsync(UserName, Password);
            if (user != null)
            {
                MessageBox.Show($"Добро пожаловать, {user.FullName}!");
            }
            else
            {
                MessageBox.Show("Неверные учетные данные");
            }
        }

        private async Task ExecuteRegister()
        {
            if (!_roleMap.TryGetValue(SelectedRole, out string dbRole))
            {
                MessageBox.Show("Выберите корректную роль");
                return;
            }
                await _userRepository.RegisterUser(UserName, Password, dbRole, FullName, PhoneNumber);   
    
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
