using PharmacyApp.Models;
using PharmacyApp.Repositories.Interfaces;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Input;

namespace PharmacyApp.ViewModel
{
    public class MedicationTypeViewModel : INotifyPropertyChanged
    {
        public ICommand AddTypeCommand { get; }
        public ICommand EditTypeCommand { get; }
        public ICommand DeleteTypeCommand { get; }
        public ICommand ClearFormCommand { get; }
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
            await _medicationTypesRepository.AddTypeItem(Name, SelectedApplicationMethod);
            await LoadData();
        }
        private async Task DeleteType()
        {
            await _medicationTypesRepository.DeleteTypeItem(SelectedType);
            await LoadData();
        }
        private async Task UpdateType()
        {
            await _medicationTypesRepository.UpdateTypeItem(SelectedType, Name, SelectedApplicationMethod);
            await LoadData();
        }
        private void FillTextBoxs()
        {
            Name = SelectedType.Name;
            SelectedApplicationMethod = SelectedType.ApplicationMethod;
        }
        private void ClearTextBoxs()
        {
            Name = null;
            SelectedApplicationMethod = null;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
