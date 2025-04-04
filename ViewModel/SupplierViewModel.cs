using Microsoft.EntityFrameworkCore;
using PharmacyApp.DTO;
using PharmacyApp.Models;
using PharmacyApp.Repositories.Interfaces;
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

        private ObservableCollection<SupplierDto> _suppliers;
        private ObservableCollection<Models.Component> _allcomponents;
        private SupplierDto _selectedSupplier;

        private string _supplierName;
        private string _contactPerson;
        private string _phone;
        private string _email;
        private sbyte _rating;
        private int _deliveryTimeDays;
        private decimal _pricePerUnit;
        private int _componentId;
        public SupplierViewModel(ISupplierRepository supplierRepository, ICrudRepository<Models.Component> crudComponent, PharmacyDbContext context)
        {
            _supplierRepository = supplierRepository;
            _crudComponent = crudComponent;
            _context = context;
            AddCommand = new RelayCommand(async () => await AddSupplier());
            DeleteCommand = new RelayCommand(async () => await DeleteSupplier());
            UpdateCommand = new RelayCommand(async () => await Updatesupplier());
            ClearCommand = new RelayCommand(ClearFields);
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
        public decimal PricePerUnit
        {
            get => _pricePerUnit;
            set
            {
                _pricePerUnit = value;
                OnPropertyChanged(nameof(PricePerUnit));
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
            try
            {

                await _supplierRepository.AddSupplierItem(SupplierName, ContactPerson, Phone, Email, Rating, DeliveryTimeDays, PricePerUnit,
                  GetSelectedComponents());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                await LoadData();
            }
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

        private async Task DeleteSupplier()
        {
            try
            {

                await _supplierRepository.DeleteSupplierItem(SelectedSupplier.SupplierId);
                await LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private async Task Updatesupplier()
        {
            await _supplierRepository.UpdateSupplierItem(SelectedSupplier.SupplierId, SupplierName, ContactPerson,
                Phone, Email, Rating, DeliveryTimeDays, PricePerUnit, GetSelectedComponents());
            await LoadData();
        }
        private void ClearFields()
        {
            SupplierName = null;
            ContactPerson = null;
            Phone = null;
            Email = null;
            Rating = 0;
            DeliveryTimeDays = 0;
            PricePerUnit = 0;
            foreach (var component in AllComponents)
            {
                component.IsSelected = false;
            }

        }
        private void FillFields()
        {
            SupplierName = SelectedSupplier.SupplierName;
            ContactPerson = SelectedSupplier.ContactPerson;
            Phone = SelectedSupplier.Phone;
            Email = SelectedSupplier.Email;
            Rating = SelectedSupplier.Rating;
            ComponentId = SelectedSupplier.ComponentId;
            DeliveryTimeDays = SelectedSupplier.DeliveryTimeDays;
            PricePerUnit = SelectedSupplier.PricePerUnit;
            MarkSelectedComponents();
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
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
