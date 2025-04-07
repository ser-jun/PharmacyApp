using PharmacyApp.DTO;
using PharmacyApp.Repositories;
using PharmacyApp.Repositories.Interfaces;
using PharmacyApp.View;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace PharmacyApp.ViewModel
{
    public class StockViewModel : INotifyPropertyChanged
    {
        public ICommand AddCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand ClearCommand { get; }
        public ICommand ApplyFilterCommand { get; }
        public ICommand ApllyExpiredComponentsCommand { get; }
        public ICommand GoMainMenuCommand { get; }  

        private IstockRepository _stockRepository;
        private StockItems _selectedItem;
        private ObservableCollection<StockItems> _stockItems;
        private string _componentName;
        private string _unitOfMeasure;
        private decimal _amount;
        private decimal _criticalNorm;
        private int _shelfLife;
        private DateTime _arrivalDate;
        private string _searchName;

        public event PropertyChangedEventHandler PropertyChanged;

        public StockViewModel(IstockRepository stockRepository)
        {
            _stockRepository = stockRepository;
            ArrivalDate = DateTime.Today;
            AddCommand = new RelayCommand(async () => await AddStockComponent());
            DeleteCommand = new RelayCommand(async () => await DeleteStockComponent());
            UpdateCommand = new RelayCommand(async () => await UpdateStockComponent());
            ClearCommand = new RelayCommand(ClearTextBoxs);
            ApplyFilterCommand = new RelayCommand(async() => await FilterByShelfLife());
            ApllyExpiredComponentsCommand = new RelayCommand(async () => await FilteredByExpiredComponents());
            GoMainMenuCommand = new RelayCommand(GoMainMenu);
            _ =LoadData();
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
                FillTextBoxs();
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
        public string SearchName
        {
            get => _searchName;
            set
            {
                _searchName = value;
                _ = SearchByName();
                OnPropertyChanged(nameof(SearchName));
            }
        }

        private async Task SearchByName()
        {
            var data = await _stockRepository.SearchByNameComponentStock(SearchName);
            StockItems = new ObservableCollection<StockItems>(data);
        }
        private async Task LoadData()
        {
            var data = await _stockRepository.LoadStockItems();
            StockItems = new ObservableCollection<StockItems>(data);
        }
        private async Task AddStockComponent()
        {
            if (string.IsNullOrWhiteSpace(ComponentName)) { MessageBox.Show("Введите название компонента"); return; }
            if (string.IsNullOrWhiteSpace(UnitOfMeasure)) { MessageBox.Show("Укажите единицы измерения"); return; }
            if (Amount <= 0) { MessageBox.Show("Количество должно быть больше 0"); return; }
            if (CriticalNorm <= 0) { MessageBox.Show("Критическая норма должна быть больше 0"); return; }
            if (ShelfLife <= 0) { MessageBox.Show("введите срок годности"); return; }
            await _stockRepository.AddComponent(ComponentName, UnitOfMeasure, Amount, CriticalNorm, ArrivalDate, ShelfLife); 
            await LoadData();
        }

        private async Task DeleteStockComponent()
        {
            if (SelectedItem == null) { MessageBox.Show("Компонент не выбран"); return; }
            await _stockRepository.DeleteItem(SelectedItem.ComponentId); await LoadData();
        }

        private async Task UpdateStockComponent()
        {
            if (SelectedItem == null) { MessageBox.Show("Компонент не выбран"); return; }
            if (string.IsNullOrWhiteSpace(ComponentName)) { MessageBox.Show("Введите название компонента"); return; }
            if (string.IsNullOrWhiteSpace(UnitOfMeasure)) { MessageBox.Show("Укажите единицы измерения"); return; }
            if (Amount <= 0) { MessageBox.Show("Количество должно быть больше 0"); return; }
            if (CriticalNorm <= 0) { MessageBox.Show("Критическая норма должна быть больше 0"); return; }
            if (ShelfLife <= 0) { MessageBox.Show("введите срок годности"); return; }
            await _stockRepository.UpdateItem(SelectedItem.ComponentId, SelectedItem.StockId, ComponentName, UnitOfMeasure, Amount, CriticalNorm, ArrivalDate, ShelfLife); await LoadData();
        }
        private void FillTextBoxs()
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
            LoadData();

        }
        private async Task FilterByShelfLife()
        {
            var data = await _stockRepository.GetFilteredDataByShelfLife();
            StockItems = new ObservableCollection<StockItems>(data);  
        }
        private async Task FilteredByExpiredComponents()
        {
            var data = await _stockRepository.GetExpiredComponents();
            StockItems = new ObservableCollection<StockItems>(data);
        }
        private void GoMainMenu()
        {
            NavigationService.OpenWindow<MainMenu>();
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}