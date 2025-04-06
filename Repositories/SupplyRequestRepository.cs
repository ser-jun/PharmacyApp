using PharmacyApp.Models;
using PharmacyApp.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using PharmacyApp.Models;

namespace PharmacyApp.Repositories
{
    public class SupplyRequestRepository : ISupplyRequestRepository
    {
        private readonly ICrudRepository<SupplyRequest> _supplyRequestCrudRepository;
        private readonly    IPendingOrderRepository _pendingOrderRepository;
        private readonly ICrudRepository<Models.Component> _crudComponentRepository;

        public SupplyRequestRepository (PharmacyDbContext context)
        {
            _supplyRequestCrudRepository = new CrudRepository<SupplyRequest>(context);
            _pendingOrderRepository = new OrderRepository(context);
        }
        public async Task<IEnumerable<SupplyRequest>> LoadSupplyRequest()
        {
            return await _supplyRequestCrudRepository.GetAllAsync();
        }
        public async Task AddSupplyRequestItem(int componentId, decimal amount, DateTime? time, string status)
        {
            var supllyRequest = new SupplyRequest
            {
                ComponentId = componentId,
                RequestedAmount = amount,
                RequestDate = time,
                Status = status
            };
            await _supplyRequestCrudRepository.AddAsync(supllyRequest);
        }
        public async Task DeleteSupplyRequestItem(SupplyRequest itemToDelete)
        {
            await _supplyRequestCrudRepository.DeleteAsync(itemToDelete);
        }
        //public async Task<IEnumerable<PendingOrder>> LoadComponentInfo()
        //{
        //   //var pendingOrder=  await _pendingOrderRepository.LoadPendingOrderInfo(); 
        //   // foreach (var component in pendingOrder)
        //   // {
        //   //     if (component.ComponentId.HasValue)
        //   //     {
        //   //         component.Component = await _crudComponentRepository.GetByIdAsync(component.ComponentId.Value);
        //   //     }
        //   // }
        //   // return pendingOrder;
        //}
        public async Task UpdateSupplyRequestItem(SupplyRequest itemToUpdate, int newcomponentId, decimal newAmount, DateTime? newTime, string newStatus)
        {
            itemToUpdate.ComponentId = newcomponentId;
            itemToUpdate.RequestedAmount = newAmount; 
            itemToUpdate.RequestDate = newTime; 
            itemToUpdate.Status = newStatus;
            await _supplyRequestCrudRepository.UpdateAsync(itemToUpdate);
        }
    }
}
