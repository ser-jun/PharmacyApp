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
    public class SupllyRequestViewModel : INotifyPropertyChanged
    {
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand ClearCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;
        private readonly ISupplyRequestRepository _supplyRequestRepository;

        private ObservableCollection<SupplyRequest> _supplyRequests;
        private SupplyRequest _selectedRequest;
        private ObservableCollection<Models.Component> _components;
        private Models.Component _selectedComponent;
        private decimal? _requestedAmount;
        private DateTime? _requestDate;
        private string _status;
        public ObservableCollection<string> Statuses { get; }
        


        public SupllyRequestViewModel(ISupplyRequestRepository supllyRequestRepository)
        {
            _supplyRequestRepository = supllyRequestRepository;
            UpdateCommand = new RelayCommand(async () => await UpdateItem());
            DeleteCommand = new RelayCommand (async () => await DeleteItem());
            ClearCommand = new RelayCommand(ClearFields);
            Statuses = new ObservableCollection<string>
            {
               "Ожидает обработки",
               "Заказана у поставщика",
            };
            _ = LoadData();
        }

        public ObservableCollection<SupplyRequest> SupplyRequests
        {
            get => _supplyRequests;
            set
            {
                _supplyRequests = value;
                OnPropertyChanged(nameof(SupplyRequests));
            }
        }
        public SupplyRequest SelectedRequest
        {
            get => _selectedRequest;
            set
            {
                _selectedRequest = value;
                OnPropertyChanged(nameof(SelectedRequest));

                if (_selectedRequest != null)
                {
                    RequestedAmount = _selectedRequest.RequestedAmount;
                    RequestDate = _selectedRequest.RequestDate;
                    Status = _selectedRequest.Status;
                }
            }
        }
        public decimal? RequestedAmount
        {
            get => _requestedAmount;
            set
            {
                _requestedAmount = value;
                OnPropertyChanged(nameof(RequestedAmount));
            }
        }
        public DateTime? RequestDate
        {
            get => _requestDate ?? DateTime.Now;
            set
            {
                _requestDate = value;
                OnPropertyChanged(nameof(RequestDate));
            }
        }
        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }

        private async Task LoadData()
        {
            var data = await _supplyRequestRepository.LoadSupplyRequest();
            SupplyRequests = new ObservableCollection<SupplyRequest>(data);
        }
        private async Task UpdateItem()
        {
            await _supplyRequestRepository.UpdateSupplyRequestItem(SelectedRequest, RequestedAmount, RequestDate, Status);
            await LoadData();
        }
        private async Task DeleteItem()
        {
            await _supplyRequestRepository.DeleteSupplyRequestItem(SelectedRequest);
            await LoadData();
        }
        private void ClearFields()
        {
            RequestedAmount = null;
            RequestDate = DateTime.Now;
            Status = null;
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
