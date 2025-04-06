using PharmacyApp.DTO;
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
    public class CustomerOrdersViewModel : INotifyPropertyChanged
    {
        public ICommand ApplyFilterCommand { get; }
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly ICustomerOrders _customerOrdersRepository;
        public ObservableCollection<string> StatusFilter { get; }
        private ObservableCollection<UnclaimedOrdersDto> _unclamedOrders;
        private string _selectedStatusFilter;
        private const string UNCLAMED = "Не пришли забрать заказ";
        private const string NEED_IT = "В ожидании";
        private int _totalCustomersWithUnclaimedOrders;
        public CustomerOrdersViewModel(ICustomerOrders customerOrdersRepository) 
        {
        _customerOrdersRepository = customerOrdersRepository;
            StatusFilter = new ObservableCollection<string>
            {
                "Не пришли забрать заказ",
                "В ожидании"
            };
            ApplyFilterCommand = new RelayCommand(async () => await LoadData());
            _ = InitializeCategoryCollection();
        }
        public int TotalCustomersWithUnclaimedOrders
        {
            get => _totalCustomersWithUnclaimedOrders;
            private set
            {
                if (_totalCustomersWithUnclaimedOrders != value)
                {
                    _totalCustomersWithUnclaimedOrders = value;
                    OnPropertyChanged(nameof(TotalCustomersWithUnclaimedOrders));
                }
            }
        }
        private ObservableCollection<MedicationCategory> _medicationCategories;
        public ObservableCollection<MedicationCategory> MedicationCategories
        {
            get => _medicationCategories;
            set
            {
                _medicationCategories = value;
                OnPropertyChanged(nameof(MedicationCategories));
            }
        }

        private MedicationCategory _selectedMedicationCategory;
        public MedicationCategory SelectedMedicationCategory
        {
            get => _selectedMedicationCategory;
            set
            {
                _selectedMedicationCategory = value;
                OnPropertyChanged(nameof(SelectedMedicationCategory));
            }
        }
        public ObservableCollection<UnclaimedOrdersDto> FilteredOrders
        {
            get => _unclamedOrders;
            set
            {
                _unclamedOrders = value;
                OnPropertyChanged(nameof(FilteredOrders));
            }
        }
        public string SelectedStatusFilter
        {
            get => _selectedStatusFilter;
            set
            {
                _selectedStatusFilter = value;
                OnPropertyChanged(nameof(SelectedStatusFilter));
            }
        }
        private async Task LoadData()
        {
            if (SelectedStatusFilter == UNCLAMED)
            {
                var data = await _customerOrdersRepository.LoadUnclamedOrders();
                FilteredOrders = new ObservableCollection<UnclaimedOrdersDto>(data);
                UpdateStatistics(data);
            }
            else if (SelectedStatusFilter == NEED_IT) 
            {
                int? categoryId = SelectedMedicationCategory?.CategoryId;
                var data = await _customerOrdersRepository.LoadWaitingMedicationInfo(categoryId);
                FilteredOrders = new ObservableCollection<UnclaimedOrdersDto>(data);
                UpdateStatistics(data);
            }
        }
        private async Task InitializeCategoryCollection()
        {
            var data = await _customerOrdersRepository.LoadMedicationCategory();
            MedicationCategories = new ObservableCollection<MedicationCategory>(data);
        }
        private void UpdateStatistics(IEnumerable<UnclaimedOrdersDto> orders)
        {
            if (orders == null || !orders.Any())
            {
                TotalCustomersWithUnclaimedOrders = 0;
                return;
            }

            TotalCustomersWithUnclaimedOrders = orders
                .Select(o => o.CustomerId)
                .Distinct()
                .Count();
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
