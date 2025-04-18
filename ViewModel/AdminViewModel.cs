﻿using PharmacyApp.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PharmacyApp.Models;
using System.Collections.ObjectModel;
using PharmacyApp.View;
using System.Windows;

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
            NavigateToMainCommand = new RelayCommand(GoMainWindow);
            LoadUsersInfo();
        }
        public User SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));
                if (SelectedUser != null)
                {
                    FillTextBoxs();
                }
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
            if (SelectedUser == null)
            {
                MessageBox.Show("Выберите элемент для удаления");
                return;
            }
           await _adminRepository.DeleteItemUser(SelectedUser.UserId);
            await LoadUsersInfo();
            ClearTextBoxs();
        }
        private async Task UpdateUser()
        {
            if (string.IsNullOrWhiteSpace(Name)) { MessageBox.Show("Введите имя пользователя"); return; }
            if (string.IsNullOrWhiteSpace(FullName)) { MessageBox.Show("Введите полное имя"); return; }
            if (string.IsNullOrWhiteSpace(Phone)) { MessageBox.Show("Введите телефон"); return; }
            if (SelectedUser == null) { MessageBox.Show("Пользователь не выбран"); return; }

            await _adminRepository.UpdateItemUser(Name, FullName, Phone, SelectedUser.UserId);
            await LoadUsersInfo();
            ClearTextBoxs();
        }
        private void FillTextBoxs()
        {
            Name = SelectedUser.Username;
            FullName=SelectedUser.FullName;
            Phone = SelectedUser.ContactPhone;
        }
        private void ClearTextBoxs()
        {
            Name = null;
            FullName = null;
            Phone = null;
        }
        private void GoMainWindow()
        {
            NavigationService.OpenWindow<AdminChooseWindow>();
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
