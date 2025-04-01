using PharmacyApp.DTO;
using PharmacyApp.Models;
using PharmacyApp.Repositories.Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace PharmacyApp.ViewModel
{
    public class AddEditMedicationWindowViewModel : INotifyPropertyChanged
    {
        public ICommand ActionCommand { get; }
        private IMedicationFormAddEdit _medicationFormAddEdit;
        public ICommand ToggleComponentCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;
        public event Action MedicationAdded;
        public event Action MedicationUpdated;

        private string _name;
        private decimal _price;
        private ObservableCollection<MedicationType> _medicationTypes;
        private ObservableCollection<MedicationCategory> _medicationCategories;
        private MedicationType _selectedType;
        private MedicationCategory _selectedCategory;
        private bool _isReadyMade;
        private ObservableCollection<Models.Component> _allComponents;


        private ObservableCollection<Models.Component> _selectedComponents = new ObservableCollection<Models.Component>();
        private Models.Component _selectedComponent;
        private Dictionary<int, decimal> _componentAmounts = new Dictionary<int, decimal>();


        private readonly MedicationsItems _medicationToEdit;
        public string WindowTitle => _medicationToEdit == null ? "Добавить лекарство" : "Редактировать лекарство";
        public string ActionButtonText => _medicationToEdit == null ? "Добавить" : "Сохранить";

        public AddEditMedicationWindowViewModel(IMedicationFormAddEdit medicationFormAdd)
         : this(medicationFormAdd, null)
        {
        }
        public AddEditMedicationWindowViewModel(
    IMedicationFormAddEdit medicationFormAdd,
    MedicationsItems medicationToEdit)
        {
            _medicationFormAddEdit = medicationFormAdd;
            _medicationToEdit = medicationToEdit;

            ActionCommand = new RelayCommand(async () => await ExecuteAction());
            ToggleComponentCommand = new RelayCommand<Models.Component>(ToggleComponent);
            InitializeData();
        }
        public ObservableCollection<Models.Component> SelectedComponents
        {
            get => _selectedComponents;
            set
            {
                _selectedComponents = value;
                OnPropertyChanged(nameof(SelectedComponents));
            }
        }

        public Models.Component SelectedComponent
        {
            get => _selectedComponent;
            set
            {
                _selectedComponent = value;
                OnPropertyChanged(nameof(SelectedComponent));
                OnPropertyChanged(nameof(SelectedComponentAmount));
            }
        }

        public decimal SelectedComponentAmount
        {
            get => _selectedComponent != null && _componentAmounts.ContainsKey(_selectedComponent.ComponentId)
                   ? _componentAmounts[_selectedComponent.ComponentId]
                   : 0;
            set
            {
                if (_selectedComponent != null)
                {
                    _componentAmounts[_selectedComponent.ComponentId] = value;
                    OnPropertyChanged(nameof(SelectedComponentAmount));
                }
            }
        }
        public ObservableCollection<Models.Component> AllComponents
        {
            get => _allComponents;
            set
            {
                _allComponents = value;
                OnPropertyChanged(nameof(AllComponents));
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
            await LoadComponents();
        }

        private void ToggleComponent(Models.Component component)
        {
            if (component == null) return;

            if (_selectedComponents.Any(c => c.ComponentId == component.ComponentId))
            {
                var toRemove = _selectedComponents.First(c => c.ComponentId == component.ComponentId);
                _selectedComponents.Remove(toRemove);
                _componentAmounts.Remove(component.ComponentId);
            }
            else
            {
                _selectedComponents.Add(component);
                _componentAmounts[component.ComponentId] = 0;
            }

            OnPropertyChanged(nameof(SelectedComponents));
        }
        private async Task LoadMedicationTypes()
        {
            var data = await _medicationFormAddEdit.LoadMedicationType();
            MedicationTypes = new ObservableCollection<MedicationType>(data);
        }
        private async Task LoadMedicationCategories()
        {
            var data = await _medicationFormAddEdit.LoadMedicationCategory();
            MedicationCategories = new ObservableCollection<MedicationCategory>(data);
        }
        private async Task AddMedication()
        {
            //await _medicationFormAddEdit.AddMedicationItem(Name, IsReadyMade, Price, SelectedType, SelectedCategory);
            MedicationAdded?.Invoke();
        }
        private async Task LoadComponents()
        {
          var data = await _medicationFormAddEdit.GetComponentList();
            AllComponents = new ObservableCollection<Models.Component>(data);
        }
        private async Task UpdateMedication ()
        {
            await _medicationFormAddEdit.UpdateMedicationItem(_medicationToEdit.MedicationId, Name, IsReadyMade, Price, SelectedType, SelectedCategory);
            MedicationUpdated?.Invoke();
        }
        private async Task ExecuteAction()
        {
            if (_medicationToEdit == null)
            {
                await AddMedication();
            }
            else
            {
                await UpdateMedication();
            }
        }
        private async void InitializeData()
        {
            await InitializeComboBox();

            if (_medicationToEdit != null)
            {
                Name = _medicationToEdit.MedicationName;
                IsReadyMade = _medicationToEdit.IsReadyMade;
                Price = _medicationToEdit.Price ?? 0;

                SelectedType = MedicationTypes?.FirstOrDefault(t => t.Name == _medicationToEdit.TypeName);

                SelectedCategory = MedicationCategories?.FirstOrDefault(c => c.Name == _medicationToEdit.CategoryName);
            }
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
