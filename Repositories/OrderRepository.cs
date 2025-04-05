using PharmacyApp.Models;
using PharmacyApp.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApp.Repositories
{
    public class OrderRepository : IOrderRepository, IOrderLoadMethods
    {
        
        private readonly ICrudRepository<Order> _crudOrderRepository;
        private readonly ICrudRepository<Prescription> _prescriptionRepository;
        private readonly ICrudRepository<Medication> _medicationRepository;
        private readonly ICrudRepository<User> _userRepository;
        public OrderRepository(PharmacyDbContext context)
        {
            _crudOrderRepository= new CrudRepository<Order>(context);
            _prescriptionRepository = new CrudRepository<Prescription>(context);
            _medicationRepository = new CrudRepository<Medication>(context);
            _userRepository = new CrudRepository<User>(context);
        }
        public async Task<IEnumerable<Order>> LoadOrdersInfo()
        {
            return  await _crudOrderRepository.GetAllAsync();
        }
        public async Task AddOrderItem(int prescriptionId, int medicationId, decimal amount, int registarId, DateTime? orderDate, 
            string status, decimal? price)
        {
            var order = new Order
            {
                PrescriptionId = prescriptionId,
                MedicationId = medicationId,
                Amount = amount,
                RegistrarId = registarId,
                OrderDate = orderDate,
                Status = status,
                Price = price
            };
            await _crudOrderRepository.AddAsync(order);
        }
        public async Task DeleteOrderItem(Order orderToDelete)
        {
            await _crudOrderRepository.DeleteAsync(orderToDelete);
        }
        public async Task UpdateOrderItem(Order orderToUpdate, int newPrescriptionId, int newMedicationId, decimal newAmount, int newRegistratId,
            DateTime? newOrderDate, string newStatus, decimal? newPrice)
        {
            orderToUpdate.PrescriptionId = newPrescriptionId;
            orderToUpdate.MedicationId = newMedicationId;
            orderToUpdate.Amount = newAmount;
            orderToUpdate.RegistrarId = newRegistratId;
            orderToUpdate.OrderDate = newOrderDate;
            orderToUpdate.Status = newStatus;
            orderToUpdate.Price = newPrice;
            await _crudOrderRepository.UpdateAsync(orderToUpdate);
        }
        
        public async Task<IEnumerable<Prescription>> LoadPrescriptionInfo()
        {
            return await _prescriptionRepository.GetAllAsync();
        }
        public async Task<IEnumerable<Medication>> LoadMedicatonInfo()
        {
            return await _medicationRepository.GetAllAsync();
        }
        public async Task<IEnumerable<User>> LoadUserInfo()
        {
            return await _userRepository.GetAllAsync();
        }
    }
}
