using Microsoft.EntityFrameworkCore.Query.Internal;
using PharmacyApp.View;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PharmacyApp.ViewModel
{
    public class MainMenuViewModel : INotifyPropertyChanged
    {
        public ICommand NavigateToMedicationsCommand { get; }
        public ICommand NavigateToOrdersCommand { get; }
        public ICommand NavigateToStockCommand { get; }
        public ICommand NavigationToSupplierRequestCommand { get; }
        public ICommand NavigateToAdministratorCommand { get; }
        public ICommand NavigateToPrescriptionCommand { get; }
        public event PropertyChangedEventHandler PropertyChanged;

        public MainMenuViewModel()
        {
            NavigateToMedicationsCommand = new RelayCommand(OpenMedicationWindow);
            NavigateToOrdersCommand = new RelayCommand(OpenOrderWindow);
            NavigateToStockCommand = new RelayCommand(OpenStockWindow);
            NavigationToSupplierRequestCommand = new RelayCommand(OpenSupplyRequestWindow);
            NavigateToAdministratorCommand = new RelayCommand(OpenAdminWindow);
            NavigateToPrescriptionCommand = new RelayCommand(OpenPrescriptionWindow);

        }

        private void OpenMedicationWindow()
        {
            NavigationService.OpenWindow<MedicationsWindow>();
        }
        private void OpenOrderWindow()
        {
            NavigationService.OpenWindow<OrderWindow>();
        }
        private void OpenStockWindow()
        {
            NavigationService.OpenWindow<StockWindow>();
        }
        private void OpenSupplyRequestWindow()
        {
            NavigationService.OpenWindow<SupplyRequestWindow>();
        }
        private void OpenAdminWindow()
        {
            NavigationService.OpenWindow<AdminChooseWindow>();
        }
        private void OpenPrescriptionWindow()
        {
            NavigationService.OpenWindow<PrescriptionWindow>();    
        }
      

    }
}
