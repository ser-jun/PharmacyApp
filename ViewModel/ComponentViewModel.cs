using PharmacyApp.Models;
using PharmacyApp.Repositories.Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace PharmacyApp.ViewModel
{
    public class ComponentViewModel : INotifyPropertyChanged
    {
        public ICommand AddCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }

        private readonly IComponentRepository _componentRepository;
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Models.Component> _components;
        private Models.Component _selectedComponent;

        private string _name;
        private string _unitOfMeasure;
        private decimal _criticalNorm;
        private int? _shelfLife;

        public ComponentViewModel(IComponentRepository componentRepository)
        {
            _componentRepository = componentRepository;

           
            AddCommand = new RelayCommand(async ()=> await AddComponent());
            SaveCommand = new RelayCommand(async () => await UpdateComponent());
            DeleteCommand = new RelayCommand(async () => await DeleteComponent());

            // Загрузка данных
            _ = LoadComponentsAsync();
        }

        public ObservableCollection<Models.Component> Components
        {
            get => _components;
            set
            {
                _components = value;
                OnPropertyChanged(nameof(Components));
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

        public string UnitOfMeasure
        {
            get => _unitOfMeasure;
            set
            {
                _unitOfMeasure = value;
                OnPropertyChanged(nameof(UnitOfMeasure));
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

        public int? ShelfLife
        {
            get => _shelfLife;
            set
            {
                _shelfLife = value;
                OnPropertyChanged(nameof(ShelfLife));
            }
        }
        public Models.Component SelectedComponent
        {
            get => _selectedComponent;
            set
            {
                _selectedComponent = value;

                FeelTextBox();
                OnPropertyChanged(nameof(SelectedComponent));
            }
        }
        private async Task LoadComponentsAsync()
        {
            var data = await _componentRepository.GetComponentList();
            Components = new ObservableCollection<Models.Component>(data);
        }

        private async Task AddComponent()
        {

            await _componentRepository.AddComponentItem(Name, UnitOfMeasure,CriticalNorm,ShelfLife??0);
            await LoadComponentsAsync();
        }
        private void FeelTextBox()
        {
            if (SelectedComponent != null)
            {
                Name = SelectedComponent.Name;
                UnitOfMeasure = SelectedComponent.UnitOfMeasure;
                CriticalNorm = SelectedComponent.CriticalNorm;
                ShelfLife = SelectedComponent.ShelfLife;
            }
        }
        private async Task UpdateComponent()
        {
            await _componentRepository.UpdateComponentItem(SelectedComponent, Name, UnitOfMeasure, CriticalNorm, ShelfLife??0);
            await LoadComponentsAsync();
        }

        private async Task DeleteComponent()
        {
            if (SelectedComponent == null) return;

            await _componentRepository.DeleteComponentItem(SelectedComponent);
            await LoadComponentsAsync();
       
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}