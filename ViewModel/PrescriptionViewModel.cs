using PharmacyApp.Models;
using PharmacyApp.Repositories.Interfaces;
using PharmacyApp.View;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace PharmacyApp.ViewModel
{
    public class PrescriptionViewModel : INotifyPropertyChanged
    {
        public ICommand AddCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand ClearCommand { get; }
        public ICommand GoBackCommand { get; }  

        public event PropertyChangedEventHandler PropertyChanged;
        private readonly IPrescriptionrepository _prescriptionRepository;
        private readonly ICrudRepository<Doctor> _doctorRepository;
        private readonly ICrudRepository<Customer> _customerRepository;

        private ObservableCollection<Customer> _customers;
        private Customer _selectedCustomer;
        private ObservableCollection<Doctor> _doctors;
        private Doctor _selectedDoctor;
        private ObservableCollection<Prescription> _prescriptions;
        private Prescription _selectedPrescription;
        private DateTime _issueDate;
        private string _diagnosis;
        private string _dosage;
        private string _duration;
        public PrescriptionViewModel(IPrescriptionrepository prescriptionRepository, ICrudRepository<Customer> customerRepository,
            ICrudRepository<Doctor> doctorRepository)
        {
            _prescriptionRepository = prescriptionRepository;
            _customerRepository = customerRepository;
            _doctorRepository = doctorRepository;
            IssueDate = DateTime.Now;
            AddCommand = new RelayCommand(async () => await AddPrescription());
            DeleteCommand = new RelayCommand(async () => await DeletePrescription());
            UpdateCommand = new RelayCommand(async () => await UpdatePrescription());
            ClearCommand = new RelayCommand(ClearFields);
            GoBackCommand = new RelayCommand(GoMainMenu);
            _ = InitializeAsync();
        }

        public ObservableCollection<Customer> Customers
        {
            get => _customers;
            set
            {
                _customers = value;
                OnPropertyChanged(nameof(Customers));
            }
        }
        public Customer SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                _selectedCustomer = value;
                OnPropertyChanged(nameof(SelectedCustomer));
            }
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
                OnPropertyChanged(nameof(SelectedDoctor));
            }
        }
        public DateTime IssueDate
        {
            get => _issueDate;
            set
            {
                _issueDate = value;
                OnPropertyChanged(nameof(IssueDate));
            }
        }
        public string Diagnosis
        {
            get => _diagnosis;
            set
            {
                _diagnosis = value;
                OnPropertyChanged(nameof(Diagnosis));
            }
        }
        public string Dosage
        {
            get => _dosage;
            set
            {
                _dosage = value;
                OnPropertyChanged(nameof(Dosage));
            }
        }
        public string Duration
        {
            get => _duration;
            set
            {
                _duration = value;
                OnPropertyChanged(nameof(Duration));
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
                FillFields();
                OnPropertyChanged(nameof(SelectedPrescription));
            }
        }
        private async Task InitializeAsync()
        {
            await LoadData();
        }
        private async Task LoadData()
        {

            var prescriptions = await _prescriptionRepository.LoadPrescriptionInfo();
            var doctors = await _doctorRepository.GetAllAsync();
            var customers = await _customerRepository.GetAllAsync();

            Application.Current.Dispatcher.Invoke(() =>
            {
                Prescriptions = new ObservableCollection<Prescription>(prescriptions);
                Doctors = new ObservableCollection<Doctor>(doctors);
                Customers = new ObservableCollection<Customer>(customers);
            });
        }
        private void FillFields()
        {
            SelectedCustomer = SelectedPrescription.Customer;
            SelectedDoctor = SelectedPrescription.Doctor;
            IssueDate = SelectedPrescription.IssueDate;
            Diagnosis = SelectedPrescription.Diagnosis;
            Dosage = SelectedPrescription.Dosage;
            Duration = SelectedPrescription.Duration;
        }
        private void ClearFields()
        {
            SelectedCustomer = null;
            SelectedDoctor = null;
            IssueDate = DateTime.Now;
            Diagnosis = null;
            Dosage = null;
            Duration = null;
        }
        private async Task AddPrescription()
        {
            if (SelectedCustomer == null) { MessageBox.Show("Выберите пациента"); return; }
            if (SelectedDoctor == null) { MessageBox.Show("Выберите врача"); return; }
            if (string.IsNullOrWhiteSpace(Diagnosis)) { MessageBox.Show("Введите диагноз"); return; }
            if (string.IsNullOrWhiteSpace(Dosage)) { MessageBox.Show("Введите дозу и лекарство"); return; }
            if (string.IsNullOrWhiteSpace(Duration)) { MessageBox.Show("Введите продолжительность приема"); return; }
            await _prescriptionRepository.AddPrescriptionItem(SelectedCustomer.CustomerId, SelectedDoctor.DoctorId, 
                IssueDate, Diagnosis, Dosage, Duration);
            await LoadData();
        }

        private async Task UpdatePrescription()
        {
            if (SelectedPrescription == null) { MessageBox.Show("Рецепт не выбран"); return; }
            if (SelectedCustomer == null) { MessageBox.Show("Выберите пациента"); return; }
            if (SelectedDoctor == null) { MessageBox.Show("Выберите врача"); return; }
            if (string.IsNullOrWhiteSpace(Diagnosis)) { MessageBox.Show("Введите диагноз"); return; }
            if (string.IsNullOrWhiteSpace(Dosage)) { MessageBox.Show("Введите дозу и лекарство"); return; }
            if (string.IsNullOrWhiteSpace(Duration)) { MessageBox.Show("Введите продолжительность приема"); return; }
            await _prescriptionRepository.UpdatePrescriptionItem(SelectedPrescription, SelectedCustomer.CustomerId,
                SelectedDoctor.DoctorId, IssueDate, Diagnosis, Dosage, Duration); 
            await LoadData();
        }

        private async Task DeletePrescription()
        {
            if (SelectedPrescription == null) { MessageBox.Show("Рецепт не выбран"); return; }
            await _prescriptionRepository.DeletePrescritionItem(SelectedPrescription);
            await LoadData();
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
