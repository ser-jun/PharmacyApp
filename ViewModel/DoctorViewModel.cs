using PharmacyApp.Models;
using PharmacyApp.Repositories.Interfaces;
using PharmacyApp.View;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace PharmacyApp.ViewModel
{
    public class DoctorViewModel : INotifyPropertyChanged
    {
        public ICommand AddDoctorCommand { get; }
        public ICommand EditDoctorCommand { get; }
        public ICommand DeleteDoctorCommand { get; }
        public ICommand ClearCommand { get; }
        public ICommand GoMenuCommand { get; }

        private readonly IDoctorRepository _doctorRepository;
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Doctor> _doctors;
        private Doctor _selectedDoctor;
        private string _fullName;
        private string _licenseNumber;
        private string _contactInfo;
        public DoctorViewModel(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
            _ = LoadDoctors();
            AddDoctorCommand = new RelayCommand(async () => await AddDoctor());
            EditDoctorCommand = new RelayCommand(async () => await UpdateDoctor());
            DeleteDoctorCommand = new RelayCommand(async () => await DeleteDoctor());
            ClearCommand = new RelayCommand(ClearTextBoxs);
            GoMenuCommand = new RelayCommand(GoMenu);
        }
        public ObservableCollection<Doctor> Doctors
        {
            get => _doctors;
            set
            {
                _doctors = value;
                OnPropertyChanged(nameof(Doctors));
            }
        }
        public Doctor SelectedDoctor
        {
            get => _selectedDoctor;
            set
            {
                _selectedDoctor = value;
                FillTextBoxs();
                OnPropertyChanged(nameof(SelectedDoctor));
            }
        }
        public string FullName
        {
            get => _fullName;
            set
            {

                _fullName = value;
                OnPropertyChanged(nameof(FullName));
            }
        }
        public string LicenseNumber
        {
            get => _licenseNumber;
            set
            {
                _licenseNumber = value;
                OnPropertyChanged(nameof(LicenseNumber));
            }
        }
        public string ContactInfo
        {
            get => _contactInfo;
            set
            {
                _contactInfo = value;
                OnPropertyChanged(nameof(ContactInfo));
            }
        }
        private async Task LoadDoctors()
        {
            var data = await _doctorRepository.LoadDoctorsInfo();
            Doctors = new ObservableCollection<Doctor>(data);
        }
        private async Task AddDoctor()
        {
            await _doctorRepository.AddDoctorItem(FullName, LicenseNumber, ContactInfo);
            await LoadDoctors();
        }
        private async Task DeleteDoctor()
        {
            await _doctorRepository.DeleteDoctorItem(SelectedDoctor);
            await LoadDoctors();
        }
        private async Task UpdateDoctor()
        {
            await _doctorRepository.UpdateDoctorItem(SelectedDoctor, FullName, LicenseNumber, ContactInfo);
            await LoadDoctors();
        }
        private void FillTextBoxs()
        {
            if (SelectedDoctor != null)
            {
                FullName = SelectedDoctor.FullName;
                LicenseNumber = SelectedDoctor.LicenseNumber;
                ContactInfo = SelectedDoctor.ContactInfo;
            }
        }
        private void ClearTextBoxs()
        {
            FullName = null;
            LicenseNumber = null;
            ContactInfo = null;
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
