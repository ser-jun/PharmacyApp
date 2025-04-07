﻿using PharmacyApp.DTO;
using PharmacyApp.Models;
using PharmacyApp.Repositories;
using PharmacyApp.Repositories.Interfaces;
using PharmacyApp.View;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace PharmacyApp.ViewModel
{
    public class MedicationsViewModel : INotifyPropertyChanged
    {
        public ICommand OpenAddWindowCommand { get; }
        public ICommand DeleteItemCommand { get; }
        public ICommand EditItemCommand { get; }
        public ICommand GomainMenuCommand { get; }

        private readonly IMedicationsRepository _medicationsRepository;
        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<MedicationsItems> _medications;
        private MedicationsItems _selectedMedication;
        private string _searchField;
        public MedicationsViewModel(IMedicationsRepository medicationsRepository)
        {
            _medicationsRepository = medicationsRepository;
            OpenAddWindowCommand = new RelayCommand(OpenFormToAdd);
            DeleteItemCommand = new RelayCommand(async () => await DeleteMedication());
            EditItemCommand = new RelayCommand(OpenFormToEdit);
            GomainMenuCommand = new RelayCommand(OpenMainMenu);
            LoadMedications();
        }
        public ObservableCollection<MedicationsItems> Medications
        {
            get => _medications;
            set
            {
                _medications = value;
                OnPropertyChanged(nameof(Medications));
            }
        }
        public MedicationsItems SelectedMedication
        {
            get => _selectedMedication;
            set
            {
                _selectedMedication = value;
                OnPropertyChanged(nameof(SelectedMedication));
            }
        }
        public string SearchField
        {
            get => _searchField;
            set
            {
                if (SearchField != value)
                {
                _searchField = value;
                OnPropertyChanged(nameof(SearchField));
                   _=SearchByName();
                }
            }
        }
        private async Task LoadMedications()
        {
            var data = await _medicationsRepository.LoadMedicationsInfo();
            Medications = new ObservableCollection<MedicationsItems>(data);
        }
        private async Task DeleteMedication()
        {
            await _medicationsRepository.DeleteMedicationItem(SelectedMedication.MedicationId);
            await LoadMedications();
        }
        private async Task SearchByName()
        {
            var data = await _medicationsRepository.SearchMedicationByName(SearchField);
            Medications = new ObservableCollection<MedicationsItems>(data);
        }
        private void OpenFormToAdd()
        {
            var addWindow = NavigationService.OpenForm<AddMedicationWindow>();
            if (addWindow.DataContext is AddEditMedicationWindowViewModel vm)
            {
                vm.MedicationAdded += async () =>
                { 
                    await LoadMedications();
                };
            }
        }
        private void OpenFormToEdit()
        {
            var editWindow = new AddMedicationWindow(SelectedMedication);

            if (editWindow.DataContext is AddEditMedicationWindowViewModel vm)
            {
                vm.MedicationUpdated += async () =>
                {
                    await LoadMedications();
                    editWindow.Close();
                };
            }

            editWindow.ShowDialog();
        }
        private void OpenMainMenu()
        {
            NavigationService.OpenWindow<MainMenu>();
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
