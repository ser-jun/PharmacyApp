using PharmacyApp.DTO;
using PharmacyApp.Repositories.Interfaces;
using PharmacyApp.View;
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
    public class MedicationsViewModel : INotifyPropertyChanged
    {
        public ICommand OpenAddWindowCommand { get; }

        private readonly IMedicationsRepository _medicationsRepository;
        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<MedicationsItems> _medications;
        public MedicationsViewModel(IMedicationsRepository medicationsRepository)
        { 
        _medicationsRepository = medicationsRepository;
            OpenAddWindowCommand = new RelayCommand(OpenAddWindow);
            LoadMedications();
        }
        public ObservableCollection<MedicationsItems> Medications
        {
            get => _medications;
            set
            {
                _medications = value;
                OnPropertyChanged(nameof(Medications));
            }
        }
        private async Task LoadMedications()
        {
            var data = await _medicationsRepository.LoadMedicationsInfo();
            Medications = new ObservableCollection<MedicationsItems>(data);
        }
        private void OpenAddWindow()
        {
            var addWindow = NavigationService.OpenForm<AddMedicationWindow>();
            if (addWindow.DataContext is AddMedicationWindowViewModel vm)
            {
                vm.MedicationAdded += async () =>
                {
                    await LoadMedications();
                };
            }
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
