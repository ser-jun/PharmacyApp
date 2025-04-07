using PharmacyApp.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PharmacyApp.ViewModel
{
    public class AdminChooseViewModel : INotifyPropertyChanged
    {
        public ICommand NavigateToUsersWindowCommand { get; }
        public ICommand NavigateToCategoryWindowCommand { get; }
        public ICommand NavigateToTypeWindowCommand { get; }
        public ICommand NavigateToDoctorsMindowCommand { get; }
        public ICommand GoBackCommand { get; }
        public event PropertyChangedEventHandler PropertyChanged;
        public AdminChooseViewModel() 
        {
            NavigateToUsersWindowCommand = new RelayCommand(GoUsersWindow);
            NavigateToTypeWindowCommand = new RelayCommand(GoTypeWindow);
            NavigateToCategoryWindowCommand = new RelayCommand(GoCategoryWindow);
            NavigateToDoctorsMindowCommand = new RelayCommand(GoDoctorsWindow);
            GoBackCommand = new RelayCommand(GoBack);
        }
        private void GoUsersWindow()
        {
            NavigationService.OpenWindow<AdminWindow>();
        }
        private void GoCategoryWindow()
        {
            NavigationService.OpenWindow<MedicationCategoryWindow>();
        }
        private void GoTypeWindow()
        {
            NavigationService.OpenWindow<MedicationTypeWindow>();
        }
        private void GoDoctorsWindow()
        {
            NavigationService.OpenWindow<DoctorWindow>();
        }
        private void GoBack()
        {
            NavigationService.OpenWindow<MainMenu>();
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
