using Microsoft.EntityFrameworkCore.ChangeTracking;
using PharmacyApp.Repositories.Interfaces;
using PharmacyApp.View;
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
        private DateTime _birthDate;
        private string _address;


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
            RegisterCommand = new RelayCommand(async () => await ExecuteRegister());
            EntryCommand = new RelayCommand(async () => await ExecuteEntry());
            PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(SelectedRole))
                {
                    OnPropertyChanged(nameof(IsCustomerSelected));
                }
            };
            BirthDate = DateTime.Now;
        }
        public DateTime BirthDate
        {
            get => _birthDate;
            set
            {
                _birthDate = value;
                OnPropertyChanged(nameof(BirthDate));
            }
        }

        public string? Address
        {
            get => _address;
            set
            {
                _address = value;
                OnPropertyChanged(nameof(Address));
            }
        }

        public bool IsCustomerSelected => SelectedRole == "Пользователь";
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
            switch (user.Role)
            {
                case "admin":
                    NavigationService.OpenWindow<AdminWindow>();
                    break;
            }
        }

        private async Task ExecuteRegister()
        {
            if (!_roleMap.TryGetValue(SelectedRole, out string dbRole))
            {
                MessageBox.Show("Выберите корректную роль");
                return;
            }
                await _userRepository.RegisterUser(UserName, Password, dbRole, FullName, PhoneNumber, BirthDate, Address);   
                ClearInputFields();
        }
        private void ClearInputFields()
        {
            UserName = null;
            Password = null;
            SelectedRole = null;
            FullName = null;
            PhoneNumber = string.Empty;
            BirthDate = DateTime.Now;
            Address = string.Empty;
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
