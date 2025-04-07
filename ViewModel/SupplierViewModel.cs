using PharmacyApp.DTO;
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
    public class SupplierViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly ISupplierRepository _supplierRepository;
        private readonly PharmacyDbContext _context;
        private readonly ICrudRepository<Models.Component> _crudComponent;
        public ICommand AddCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand ClearCommand { get; }
        public ICommand ApplyFilter { get; }
        public ICommand GoBackCommand { get; }

        private ObservableCollection<SupplierDto> _suppliers;
        private ObservableCollection<Models.Component> _allcomponents;
        private SupplierDto _selectedSupplier;

        private string _supplierName;
        private string _contactPerson;
        private string _phone;
        private string _email;
        private sbyte _rating;
        private int _deliveryTimeDays;
        private int _componentId;
        private byte? _filterRating;
        private string _searchFiled;
        public SupplierViewModel(ISupplierRepository supplierRepository, ICrudRepository<Models.Component> crudComponent, PharmacyDbContext context)
        {
            _supplierRepository = supplierRepository;
            _crudComponent = crudComponent;
            _context = context;
            AddCommand = new RelayCommand(async () => await AddSupplier());
            DeleteCommand = new RelayCommand(async () => await DeleteSupplier());
            UpdateCommand = new RelayCommand(async () => await Updatesupplier());
            ClearCommand = new RelayCommand(ClearFields);
            ApplyFilter = new RelayCommand(async () => await LoadFilteredInfoByRatinOrComponent());
            GoBackCommand = new RelayCommand(GoMenu);
            LoadData().ConfigureAwait(false);
        }
        public ObservableCollection<SupplierDto> Suppliers
        {
            get => _suppliers;
            set
            {
                _suppliers = value;
                OnPropertyChanged(nameof(Suppliers));
            }
        }
        public ObservableCollection<Models.Component> AllComponents
        {
            get => _allcomponents;
            set
            {
                _allcomponents = value;
                OnPropertyChanged(nameof(AllComponents));
            }
        }
        public SupplierDto SelectedSupplier
        {
            get => _selectedSupplier;
            set
            {
                _selectedSupplier = value;
                FillFields();
                OnPropertyChanged(nameof(SelectedSupplier));
            }
        }
        public string SupplierName
        {
            get => _supplierName;
            set
            {
                _supplierName = value;
                OnPropertyChanged(nameof(SupplierName));
            }
        }
        public string ContactPerson
        {
            get => _contactPerson;
            set
            {
                _contactPerson = value;
                OnPropertyChanged(nameof(ContactPerson));
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
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        public sbyte Rating
        {
            get => _rating;
            set
            {
                _rating = value;
                OnPropertyChanged(nameof(Rating));
            }
        }
        public int DeliveryTimeDays
        {
            get => _deliveryTimeDays;
            set
            {
                _deliveryTimeDays = value;
                OnPropertyChanged(nameof(DeliveryTimeDays));
            }
        }
        public int ComponentId
        {
            get => _componentId;
            set
            {
                _componentId = value;
                OnPropertyChanged(nameof(ComponentId));
            }
        }
        public byte? FilterRating
        {
            get => _filterRating;
            set
            {
                _filterRating = value;
                OnPropertyChanged(nameof(FilterRating));
            }
        }
        public string SearchFiled
        {
            get => _searchFiled;
            set
            {
                if (SearchFiled != value)
                {

                    _searchFiled = value;
                    OnPropertyChanged(nameof(SearchFiled));
                    _ = SearchByName();
                }

            }
        }
        private async Task LoadData()
        {
            try
            {
                var suppliers = await _supplierRepository.LoadSupplierInfo();
                Suppliers = new ObservableCollection<SupplierDto>(suppliers);

                var components = await _crudComponent.GetAllAsync();
                AllComponents = new ObservableCollection<Models.Component>(components);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async Task AddSupplier()
        {
            if (string.IsNullOrWhiteSpace(SupplierName)) { MessageBox.Show("Введите название поставщика"); return; }
            if (string.IsNullOrWhiteSpace(ContactPerson)) { MessageBox.Show("Введите имя ответственного"); return; }
            if (string.IsNullOrWhiteSpace(Phone)) { MessageBox.Show("Введите телефон"); return; }
            if (string.IsNullOrWhiteSpace(Email)) { MessageBox.Show("Введите почту"); return; }
            if (GetSelectedComponents().Count == 0) { MessageBox.Show("Выберите компоненты"); return; }
            if (Rating ==0 ) { MessageBox.Show("Введите рейтинг"); return; }
            await _supplierRepository.AddSupplierItem(SupplierName, ContactPerson, Phone, Email, Rating,
                DeliveryTimeDays, GetSelectedComponents());
            await LoadData();
        }

        private List<Models.Component> GetSelectedComponents()
        {
            return AllComponents
                .Where(c => c.IsSelected)
                .Select(c => new Models.Component
                {
                    ComponentId = c.ComponentId,
                    Name = c.Name
                })
                .ToList();
        }
        private List<int> GetSelectedComponentIds()
        {
            return AllComponents
                .Where(c => c.IsSelected)
                .Select(c => c.ComponentId)
                .ToList();
        }

        private async Task DeleteSupplier()
        {
            if (SelectedSupplier == null)
            {
                MessageBox.Show("Выберите поставщика для удаления");
                return;
            }

            try
            {
               
                using (var localContext = new PharmacyDbContext())
                {
                    var localRepository = new SupplierRepository(localContext); 
                    await localRepository.DeleteSupplierItem(SelectedSupplier.SupplierId);
                }

                ClearFields();
                await LoadData(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении: {ex.Message}");
            }
        }
        private async Task Updatesupplier()
        {
            if (SelectedSupplier == null) { MessageBox.Show("Поставщик не выбран"); return; }
            if (string.IsNullOrWhiteSpace(SupplierName)) { MessageBox.Show("Введите название поставщика"); return; }
            if (string.IsNullOrWhiteSpace(ContactPerson)) { MessageBox.Show("Введите имя ответственного"); return; }
            if (string.IsNullOrWhiteSpace(Phone)) { MessageBox.Show("Введите телефон"); return; }
            if (string.IsNullOrWhiteSpace(Email)) { MessageBox.Show("Введите почту"); return; }
            if (GetSelectedComponents().Count == 0) { MessageBox.Show("Выберите компоненты"); return; }
            if (Rating == 0) { MessageBox.Show("Введите рейтинг"); return; }
            await _supplierRepository.UpdateSupplierItem(SelectedSupplier.SupplierId, SupplierName, ContactPerson, Phone, Email, Rating, DeliveryTimeDays, GetSelectedComponents()); await LoadData();
        }
        private void ClearFields()
        {
            SupplierName = null;
            ContactPerson = null;
            Phone = null;
            Email = null;
            Rating = 0;
            DeliveryTimeDays = 0;
            FilterRating = 0;
            foreach (var component in AllComponents)
            {
                component.IsSelected = false;
            }
            LoadData();

        }
        private void FillFields()
        {
            if (SelectedSupplier == null)
            {
                ClearFields(); 
                return;
            }
            SupplierName = SelectedSupplier.SupplierName;
            ContactPerson = SelectedSupplier.ContactPerson;
            Phone = SelectedSupplier.Phone;
            Email = SelectedSupplier.Email;
            Rating = SelectedSupplier.Rating;
            ComponentId = SelectedSupplier.ComponentId;
            DeliveryTimeDays = SelectedSupplier.DeliveryTimeDays;
            MarkSelectedComponents();
        }
        private async Task LoadFilteredInfoByRatinOrComponent()
        {
            var selectedIds = GetSelectedComponentIds();
            var data = await _supplierRepository.FilterByRatingOrComponent(selectedIds.Any() ? selectedIds : null, FilterRating);
            Suppliers = new ObservableCollection<SupplierDto>(data);
        }
        private void MarkSelectedComponents()
        {
            foreach (var component in AllComponents)
            {
                component.IsSelected = false;
            }

            var supplierComponents = _context.SupplierComponents
             .Where(sc => sc.SupplierId == SelectedSupplier.SupplierId)
             .Select(sc => sc.ComponentId)
             .ToList();

            foreach (var component in AllComponents)
            {
                component.IsSelected = supplierComponents.Contains(component.ComponentId);
            }
        }
        private async Task SearchByName()
        {
            if (SearchFiled != string.Empty)
            {
            var data = await _supplierRepository.SearchByName(SearchFiled.Trim().ToLower());
            Suppliers = new ObservableCollection<SupplierDto>(data);
            }
            else
            {
                await LoadData();
            }
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
