using PharmacyApp.DTO;
using PharmacyApp.Repositories;
using PharmacyApp.Repositories.Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace PharmacyApp.ViewModel
{
    public class StockViewModel : INotifyPropertyChanged
    {
        public ICommand AddCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand ClearCommand { get; }

        private IstockRepository _stockRepository;
        private StockItems _selectedItem;
        private ObservableCollection<StockItems> _stockItems;
        private string _componentName;
        private string _unitOfMeasure;
        private decimal _amount;
        private decimal _criticalNorm;
        private int _shelfLife;
        private DateTime _arrivalDate;

        public event PropertyChangedEventHandler PropertyChanged;

        public StockViewModel(IstockRepository stockRepository)
        {
            _stockRepository = stockRepository;
            ArrivalDate = DateTime.Today;
            AddCommand = new RelayCommand(async () => await AddStockComponent());
            DeleteCommand = new RelayCommand(async () => await DeleteStockComponent());
            UpdateCommand = new RelayCommand(async () => await UpdateStockComponent());
            ClearCommand = new RelayCommand(ClearTextBoxs);
            _=LoadData();
        }

        public ObservableCollection<StockItems> StockItems
        {
            get => _stockItems;
            set
            {
                _stockItems = value;
                OnPropertyChanged(nameof(StockItems));
            }
        }
        public StockItems SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                FeelTextBoxs();
                OnPropertyChanged(nameof(SelectedItem));
            }
        }
        public string ComponentName
        {
            get => _componentName;
            set
            {
                _componentName = value;
                OnPropertyChanged(nameof(ComponentName));
            }
        }
        public string UnitOfMeasure
        {
            get => _unitOfMeasure;
            set
            {
                _unitOfMeasure = value;
                OnPropertyChanged(nameof(UnitOfMeasure));
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
        public decimal CriticalNorm
        {
            get => _criticalNorm;
            set
            {
                _criticalNorm = value;
                OnPropertyChanged(nameof(CriticalNorm));
            }
        }
        public int ShelfLife
        {
            get => _shelfLife;
            set
            {
                _shelfLife = value;
                    OnPropertyChanged(nameof(ShelfLife));
            }
        }
        public DateTime ArrivalDate
        {
            get => _arrivalDate;
            set
            {
                _arrivalDate = value;
                OnPropertyChanged(nameof(ArrivalDate));
            }
        }


        private async Task LoadData()
        {
            var data = await _stockRepository.LoadStockItems();
            StockItems = new ObservableCollection<StockItems>(data);
        }
        private async Task AddStockComponent ()
        {
            await _stockRepository.AddComponent(ComponentName, UnitOfMeasure, Amount, CriticalNorm, ArrivalDate, ShelfLife);
            await LoadData();
        }
        private async Task DeleteStockComponent()
        {
            await _stockRepository.DeleteItem(SelectedItem.ComponentId);
            await LoadData();
        }
        private async Task UpdateStockComponent()
        {
            await _stockRepository.UpdateItem(SelectedItem.ComponentId, SelectedItem.StockId, ComponentName, UnitOfMeasure, Amount, CriticalNorm,
                ArrivalDate, ShelfLife);
            await LoadData();
        }
        private void FeelTextBoxs()
        {
            if (SelectedItem != null)
            {
                ComponentName = SelectedItem.ComponentName;
                UnitOfMeasure = SelectedItem.UnitOfMeasure;
                Amount = SelectedItem.Amount;
                CriticalNorm = SelectedItem.CriticalNorm;
                ArrivalDate = SelectedItem.ArrivalDate;
                ShelfLife = SelectedItem.ShelfLife;
            }
        }
        private void ClearTextBoxs()
        {
            ComponentName = null;
            UnitOfMeasure = null;
            Amount = 0;
            CriticalNorm = 0;
            ArrivalDate = DateTime.Now;
            ShelfLife = 0;

        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}