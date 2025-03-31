using PharmacyApp.Models;
using PharmacyApp.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PharmacyApp.ViewModel
{
    public class AddMedicationWindowViewModel : INotifyPropertyChanged
    {
        public ICommand AddEntryCommand { get; }
        private IMedicationFormAdd _medicationFormAdd;
        public event PropertyChangedEventHandler PropertyChanged;
        public event Action MedicationAdded;

        private string _name;
        private decimal _price;
        private ObservableCollection<MedicationType> _medicationTypes;
        private ObservableCollection<MedicationCategory> _medicationCategories;
        private MedicationType _selectedType;
        private MedicationCategory _selectedCategory;
        private bool _isReadyMade;
        public AddMedicationWindowViewModel(IMedicationFormAdd medicationFormAdd) 
        {
        _medicationFormAdd = medicationFormAdd;
            AddEntryCommand = new RelayCommand(async () => await AddMedication());
            InitializeComboBox().ConfigureAwait(false);

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
        public decimal Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged(nameof(Price));
            }
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
                OnPropertyChanged(nameof(SelectedType));
            }
        }
        public ObservableCollection<MedicationCategory> MedicationCategories
        {
            get => _medicationCategories;
            set
            {
                _medicationCategories = value;
                OnPropertyChanged(nameof(MedicationCategories));
            }
        }
        public MedicationCategory SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged(nameof(SelectedCategory));
            }
        }
        public bool IsReadyMade
        {
            get => _isReadyMade;
            set
            {
                _isReadyMade = value;
                OnPropertyChanged(nameof(IsReadyMade));
            }
        }
        private async Task InitializeComboBox()
        {
        await LoadMedicationCategories();
            await LoadMedicationTypes();    
        }
        private async Task LoadMedicationTypes()
        {
            var data = await _medicationFormAdd.LoadMedicationType();
            MedicationTypes = new ObservableCollection<MedicationType>(data);
        }
        private async Task LoadMedicationCategories()
        {
            var data = await _medicationFormAdd.LoadMedicationCategory();
            MedicationCategories = new ObservableCollection<MedicationCategory>(data);
        }
        public async Task AddMedication()
        {
           await _medicationFormAdd.AddMedicationItem(Name, IsReadyMade, Price, SelectedType, SelectedCategory);
            MedicationAdded?.Invoke();
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
