using PharmacyApp.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PharmacyApp.Models;
using System.Collections.ObjectModel;

namespace PharmacyApp.ViewModel
{
    public class AdminViewModel : INotifyPropertyChanged
    {
        public ICommand EditUserCommand { get; }
        public ICommand DeleteItemUserCommand { get; }
        public ICommand NavigateBackCommand { get; }
        public ICommand NavigateToMainCommand { get; }
        public ICommand NavigateForwardCommand { get; }

        private User _selectedUser;
        private ObservableCollection<User> _users;
        private string _name;
        private string _fullName;
        private string _phone;

        private readonly IAdminRepository _adminRepository;
        public event PropertyChangedEventHandler PropertyChanged;

        public AdminViewModel(IAdminRepository adminRepository) 
        {
        _adminRepository = adminRepository;
            DeleteItemUserCommand = new RelayCommand(async () => await DeleteUser());
            EditUserCommand = new RelayCommand (async () => await UpdateUser());    
            LoadUsersInfo();
        }
        public User SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));
            }
        }
        public ObservableCollection<User> Users
        {
            get => _users;
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
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
        public string Phone
        {
            get => _phone;
            set
            {
                _phone = value;
                OnPropertyChanged(nameof(Phone));
            }
        }

        private async Task LoadUsersInfo()
        {
         var data=  await _adminRepository.LoadUsers();
            Users = new ObservableCollection<User>(data);
        }
        private async Task DeleteUser()
        {
           await _adminRepository.DeleteItemUser(SelectedUser.UserId);
            await LoadUsersInfo();
        }
        private async Task UpdateUser()
        {
            await _adminRepository.UpdateItemUser(Name, FullName, Phone, SelectedUser.UserId);
            await LoadUsersInfo();
        }

        private void GoForwardWindow()
        {

        }
        private void GoMainWindow()
        {
            NavigationService.OpenWindow<MainWindow>();
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
