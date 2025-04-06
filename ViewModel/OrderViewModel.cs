using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PharmacyApp.Models;
using PharmacyApp.Repositories.Interfaces;
using PharmacyApp.View;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace PharmacyApp.ViewModel
{
    public class OrderViewModel : INotifyPropertyChanged
    {
        public ICommand AddCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand ClearCommand { get; }
        public ICommand OpenPendingOrderComponent { get; }
        public ICommand OpenSupplyRequestCommand {  get; }  


        private readonly IOrderRepository _orderRepository;
        private readonly IOrderLoadMethods _loadMethods;
        private readonly IPendingOrderRepository _pendingOrderRepository;
        private readonly ICrudRepository<Models.Component> _crudRepository;

        private ObservableCollection<Order> _orders;
        private Order _selectedOrder;
        private ObservableCollection<Prescription> _prescriptions;
        private Prescription _selectedPrescription;
        private ObservableCollection<Medication> _medications;
        private Medication _selectedMedication;
        private ObservableCollection<User> _registrars;
        private ObservableCollection<Models.Component> _components;
        private Models.Component _selectedComponent;
        private ObservableCollection<PendingOrder> _pendingOrders;
        private User _selectedRegistrar;
        private string _status;
        private decimal _amount;
        private decimal? _price;
        private DateTime? _date;
        public decimal? _componentAmount;
        public event PropertyChangedEventHandler PropertyChanged;
        private const string ROLE_PHARMACIST = "pharmacist";
        public OrderViewModel(IOrderRepository orderRepository, IOrderLoadMethods orderLoadsMethods)
        {
            _orderRepository = orderRepository;
            _loadMethods = orderLoadsMethods;
            _ = Initialize();
            OrderDate = DateTime.Now;
            OrderStatuses = new ObservableCollection<string>
            {
                "Ожидает обработки",
                "В процессе изготовления",
                "Ожидает компонентов",
                "Готов к выдаче",
                "Выдан пациенту"
            };
            AddCommand = new RelayCommand(async () => await AddOrder());
            UpdateCommand = new RelayCommand(async () => await UpdateOrder());
            DeleteCommand = new RelayCommand(async () => await DeleteOrder());
            ClearCommand = new RelayCommand(ClearFields);
            OpenPendingOrderComponent = new RelayCommand(OpenPendingOrderInfo);
            OpenSupplyRequestCommand = new RelayCommand(OpenSupplyRequestWindow);
        }

        public OrderViewModel(IPendingOrderRepository pendingOrderRepository, ICrudRepository<Models.Component> component)
        {
            _pendingOrderRepository = pendingOrderRepository;
            _crudRepository = component;
            _ = LoadPendingOrder();

        }
        #region Properties
        public ObservableCollection<PendingOrder> PendingOrders
        {
            get=> _pendingOrders;
            set
            {
                _pendingOrders = value;
                OnPropertyChanged(nameof(PendingOrders));
            }
        }
        public ObservableCollection<Order> Orders
        {
            get => _orders;
            set
            {
                _orders = value;
                OnPropertyChanged(nameof(Orders));
            }
        }
        public Order SelectedOrder
        {
            get => _selectedOrder;
            set
            {
                _selectedOrder = value;
                OnPropertyChanged(nameof(SelectedOrder));
                if (value == null)
                {
                    SelectedPrescription = null;
                    SelectedMedication = null;
                    SelectedRegistrar = null;
                    Status = null;
                    Amount = 0;
                    Price = 0;
                }
                else
                {
                    SelectedPrescription = value.Prescription;
                    SelectedMedication = value.Medication;
                    SelectedRegistrar = value.Registrar;
                    Status = value.Status;
                    Amount = value.Amount;
                    Price = value.Price;
                }
            }
        }
        public ObservableCollection<Prescription> Prescriptions
        {
            get => _prescriptions;
            set
            {
                _prescriptions = value;
                OnPropertyChanged(nameof(Prescriptions));
            }
        }
        public Prescription SelectedPrescription
        {
            get => _selectedPrescription;
            set
            {
                _selectedPrescription = value;
                OnPropertyChanged(nameof(SelectedPrescription));
            }
        }
        public ObservableCollection<Medication> Medications
        {
            get => _medications;
            set
            {
                _medications = value;
                OnPropertyChanged(nameof(Medications));
            }
        }
        public Medication SelectedMedication
        {
            get => _selectedMedication;
            set
            {
                _selectedMedication = value;
                OnPropertyChanged(nameof(SelectedMedication));
            }
        }
        public ObservableCollection<User> Registrars
        {
            get => _registrars;
            set
            {
                _registrars = value;
                OnPropertyChanged(nameof(Registrars));
            }
        }
        public User SelectedRegistrar
        {
            get => _selectedRegistrar;
            set
            {
                _selectedRegistrar = value;
                OnPropertyChanged(nameof(SelectedRegistrar));
            }
        }
        public ObservableCollection<string> OrderStatuses { get; }
        public ObservableCollection<Models.Component> Components
        {
            get => _components;
            set
            {
                _components = value;
                OnPropertyChanged(nameof(Components));
            }
        }
        public Models.Component SelectedComponent
        {
            get => _selectedComponent;
            set
            {
                _selectedComponent = value;
                OnPropertyChanged(nameof(SelectedComponent));
            }
        }
        public decimal? ComponentAmount
        {
            get => _componentAmount;
            set
            {
                _componentAmount = value;
                OnPropertyChanged(nameof(ComponentAmount));
            }
        }
        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }
        public decimal Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                OnPropertyChanged(nameof(Amount));
            }
        }
        public decimal? Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged(nameof(Price));
            }
        }
        public DateTime? OrderDate
        {
            get => _date;
            set
            {
                _date = value;
                OnPropertyChanged(nameof(OrderDate));
            }
        }
        #endregion
        private async Task Initialize()
        {
            await LoadData();
        }
        private async Task LoadData()
        {
            var orderData = await _orderRepository.LoadOrdersInfo();
            var medicationData = await _loadMethods.LoadMedicatonInfo();
            var userData = (await _loadMethods.LoadUserInfo()).Where(c => c.Role == ROLE_PHARMACIST);
            var prescriptionData = await _loadMethods.LoadPrescriptionInfo();
            var componentsData = await _loadMethods.LoadComponentInfo();
            
            Application.Current.Dispatcher.Invoke(() =>
            {
                Orders = new ObservableCollection<Order>(orderData);
                Prescriptions = new ObservableCollection<Prescription>(prescriptionData);
                Medications = new ObservableCollection<Medication>(medicationData);
                Registrars = new ObservableCollection<User>(userData);
                Components = new ObservableCollection<Models.Component>(componentsData);
            });
        }
        private async Task LoadPendingOrder()
        {
            var pendingOrders = await _pendingOrderRepository.LoadPendingOrderInfo();
            PendingOrders = new ObservableCollection<PendingOrder>(pendingOrders);
        }
        private async Task AddOrder()
        {
            
            await _orderRepository.AddOrderItem(SelectedPrescription.PrescriptionId,SelectedMedication.MedicationId,Amount,
                SelectedRegistrar.UserId, OrderDate, Status,Price, SelectedComponent, ComponentAmount);
            await LoadData();
        }
        private async Task DeleteOrder()
        {
            await _orderRepository.DeleteOrderItem(SelectedOrder); 
            await LoadData();
        
        }
        private async Task UpdateOrder()
        {
            await _orderRepository.UpdateOrderItem(SelectedOrder, SelectedPrescription.PrescriptionId, SelectedMedication.MedicationId,
                Amount, SelectedRegistrar.UserId, OrderDate, Status, Price, SelectedComponent, ComponentAmount);
            await LoadData();
        }
        private async Task LoadUnclamedOrder()
        {
           var data = await _loadMethods.LoadUnclamedOrders();
        }
        private void ClearFields()
        {
            SelectedPrescription = null;
            SelectedMedication = null;  
            SelectedRegistrar = null;
            Status = null;
            Amount = 0;
            Price = 0;
            OrderDate = DateTime.Now;
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void OpenPendingOrderInfo()
        {
            NavigationService.OpenForm<PendingOrderWindow>();
        }
        private void OpenSupplyRequestWindow()
        {
            NavigationService.OpenForm<SupplyRequestWindow>();
        }

    }
}
