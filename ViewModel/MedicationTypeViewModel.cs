using PharmacyApp.Models;
using PharmacyApp.Repositories.Interfaces;
using PharmacyApp.View;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows;
using System.Windows.Input;

namespace PharmacyApp.ViewModel
{
    public class MedicationTypeViewModel : INotifyPropertyChanged
    {
        public ICommand AddTypeCommand { get; }
        public ICommand EditTypeCommand { get; }
        public ICommand DeleteTypeCommand { get; }
        public ICommand ClearFormCommand { get; }
        public ICommand GoMenuCommand { get; }
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly IMedicationTypesRepository _medicationTypesRepository;
        private MedicationType _selectedType;
        private string _name;
        private string _selectedApplicationMethod;

        public ObservableCollection<MedicationType> _medicationTypes;
        public ObservableCollection<string> ApplicationMethods { get; } = new ObservableCollection<string>
        {
            "Внутрь",
            "Наружное",
            "Смешанное"
        };
        public MedicationTypeViewModel(IMedicationTypesRepository medicationTypesRepository)
        {
            _medicationTypesRepository = medicationTypesRepository;
            AddTypeCommand = new RelayCommand(async () => await AddType());
            DeleteTypeCommand = new RelayCommand(async () => await DeleteType());
            EditTypeCommand = new RelayCommand(async () => await UpdateType());
            ClearFormCommand = new RelayCommand(ClearTextBoxs);
            GoMenuCommand = new RelayCommand(GoMenu);
            _ = LoadData();
        }
        
        public ObservableCollection<MedicationType> MedicationTypes
        {
            get => _medicationTypes;
            set
            {
                _medicationTypes = value;
                OnPropertyChanged(nameof(MedicationTypes));
            }
        }
        public MedicationType SelectedType
        {
            get => _selectedType;
            set
            {
                    _selectedType = value;
                FillTextBoxs();
                    OnPropertyChanged(nameof(SelectedType));
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
        public string SelectedApplicationMethod
        {
            get => _selectedApplicationMethod;
            set
            {
                    _selectedApplicationMethod = value;
                    OnPropertyChanged(nameof(SelectedApplicationMethod));
            }
        }

        private async Task LoadData()
        {
            var data = await _medicationTypesRepository.LoadTypesInfo();
            MedicationTypes = new ObservableCollection<MedicationType>(data);
        }
        private async Task AddType()
        {
            if (string.IsNullOrWhiteSpace(Name)) { MessageBox.Show("Введите название типа"); return; }
            if (SelectedApplicationMethod == null) { MessageBox.Show("Выберите метод применения"); return; }
            await _medicationTypesRepository.AddTypeItem(Name, SelectedApplicationMethod); await LoadData();
        }

        private async Task DeleteType()
        {
            if (SelectedType == null) { MessageBox.Show("Тип не выбран"); return; }
            await _medicationTypesRepository.DeleteTypeItem(SelectedType); await LoadData();
        }

        private async Task UpdateType()
        {
            if (SelectedType == null) { MessageBox.Show("Тип не выбран"); return; }
            if (string.IsNullOrWhiteSpace(Name)) { MessageBox.Show("Введите название типа"); return; }
            if (SelectedApplicationMethod == null) { MessageBox.Show("Выберите метод применения"); return; }
            await _medicationTypesRepository.UpdateTypeItem(SelectedType, Name, SelectedApplicationMethod); await LoadData();
        }
        private void FillTextBoxs()
        {
            if(SelectedType == null)
            {
                return;
            }
            Name = SelectedType.Name;
            SelectedApplicationMethod = SelectedType.ApplicationMethod;
        }
        private void ClearTextBoxs()
        {
            Name = null;
            SelectedApplicationMethod = null;
        }
        private void GoMenu()
        {
            NavigationService.OpenWindow<AdminChooseWindow>();
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
