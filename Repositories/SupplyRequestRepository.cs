using PharmacyApp.Models;
using PharmacyApp.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using PharmacyApp.Models;
using Microsoft.EntityFrameworkCore;

namespace PharmacyApp.Repositories
{
    public class SupplyRequestRepository : ISupplyRequestRepository
    {
        private readonly ICrudRepository<SupplyRequest> _supplyRequestCrudRepository;
        private readonly    IPendingOrderRepository _pendingOrderRepository;
        private readonly ICrudRepository<Models.Component> _crudComponentRepository;
        private readonly PharmacyDbContext _context;

        public SupplyRequestRepository (PharmacyDbContext context)
        {
            _context = context;
            _supplyRequestCrudRepository = new CrudRepository<SupplyRequest>(context);
            _pendingOrderRepository = new OrderRepository(context);
        }
        public async Task<IEnumerable<SupplyRequest>> LoadSupplyRequest()
        {
            return await _context.SupplyRequests
                       .Include(sr => sr.Component) 
                       .ToListAsync();
        }

        public async Task DeleteSupplyRequestItem(SupplyRequest itemToDelete)
        {
            await _supplyRequestCrudRepository.DeleteAsync(itemToDelete);
        }

        public async Task UpdateSupplyRequestItem(SupplyRequest itemToUpdate, decimal? newAmount, DateTime? newTime, string newStatus)
        {
            itemToUpdate.RequestedAmount = newAmount; 
            itemToUpdate.RequestDate = newTime; 
            itemToUpdate.Status = newStatus;
            await _supplyRequestCrudRepository.UpdateAsync(itemToUpdate);
        }
    }
}
