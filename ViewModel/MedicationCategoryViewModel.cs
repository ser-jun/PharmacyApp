using PharmacyApp.Models;
using PharmacyApp.Repositories.Interfaces;
using PharmacyApp.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PharmacyApp.ViewModel
{
    public class MedicationCategoryViewModel : INotifyPropertyChanged
    {
        public ICommand AddCategoryCommand { get; }
        public ICommand EditCategoryCommand { get; }
        public ICommand DeleteCategoryCommand { get; }
        public ICommand ClearTextBoxsCommand { get; }
        public ICommand GoBackCommand { get; }  

        public event PropertyChangedEventHandler PropertyChanged;
        private readonly IMedicationCategoryRepository _categoryRepository;
        private MedicationCategory _selectedCategory;
        private string _name;
        private string _description;

        public ObservableCollection<MedicationCategory> _categories;
        public MedicationCategoryViewModel(IMedicationCategoryRepository medicationRepositoryRepository)
        {
            _categoryRepository = medicationRepositoryRepository;
            AddCategoryCommand = new RelayCommand(async () => await AddCategory());
            EditCategoryCommand = new RelayCommand(async() => await UpdateCategory());
            DeleteCategoryCommand = new RelayCommand(async () => await DeleteCategory());
            ClearTextBoxsCommand = new RelayCommand(ClearTextBoxs);
            GoBackCommand = new RelayCommand(GoMenu);
            _ =LoadData();
        }
        public MedicationCategory SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                    _selectedCategory = value;
                FillTextBoxs();
                    OnPropertyChanged(nameof(SelectedCategory));
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
        public string Description
        {
            get => _description;
            set
            {
                    _description = value;
                    OnPropertyChanged(nameof(Description));
            }
        }
        public ObservableCollection<MedicationCategory> Categories
        {
            get => _categories;
            set
            {
                _categories = value;
                OnPropertyChanged(nameof(Categories));
            }
        }
        private async Task LoadData()
        {
           var data= await _categoryRepository.LoadCategoriesInfo();
            Categories = new ObservableCollection<MedicationCategory>(data);    
        }
        private async Task AddCategory()
        {
            if (string.IsNullOrWhiteSpace(Name)) { MessageBox.Show("Введите название категории"); return; }
            if (string.IsNullOrWhiteSpace(Description)) { MessageBox.Show("Введите описание"); return; }
            await _categoryRepository.AddCategoryItem(Name, Description); await LoadData();
        }

        private async Task DeleteCategory()
        {
            if (SelectedCategory == null) { MessageBox.Show("Категория для удаления не выбрана"); return; }
            await _categoryRepository.DeleteCategoryItem(SelectedCategory); await LoadData();
        }

        private async Task UpdateCategory()
        {
            if (SelectedCategory == null) { MessageBox.Show("Категория для обновления не выбрана"); return; }
            if (string.IsNullOrWhiteSpace(Name)) { MessageBox.Show("Введите название категории"); return; }
            if (string.IsNullOrWhiteSpace(Description)) { MessageBox.Show("Введите описание"); return; }
            await _categoryRepository.UpdateCategoryItem(SelectedCategory, Name, Description); await LoadData();
        }
        private void FillTextBoxs()
        {
            if (SelectedCategory == null)
            {
                return;
            }
            Name = SelectedCategory.Name;
            Description = SelectedCategory.Description;
        }
        private void ClearTextBoxs()
        {
            Name = null;
            Description = null;
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
