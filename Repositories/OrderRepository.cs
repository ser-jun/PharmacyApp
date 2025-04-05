﻿using PharmacyApp.Models;
using PharmacyApp.Repositories.Interfaces;

namespace PharmacyApp.Repositories
{
    public class OrderRepository : IOrderRepository, IOrderLoadMethods
    {

        private readonly ICrudRepository<Order> _crudOrderRepository;
        private readonly ICrudRepository<Prescription> _prescriptionRepository;
        private readonly ICrudRepository<Medication> _medicationRepository;
        private readonly ICrudRepository<User> _userRepository;
        private readonly ICrudRepository<Models.Component> _componentRepository;
        private readonly ICrudRepository<PendingOrder> _pendingOrderRepository;
        private const string ORDER_STATUS = "Ожидает компонентов";
        private string statusBeforeUpdate;
        public OrderRepository(PharmacyDbContext context)
        {
            _crudOrderRepository = new CrudRepository<Order>(context);
            _prescriptionRepository = new CrudRepository<Prescription>(context);
            _medicationRepository = new CrudRepository<Medication>(context);
            _userRepository = new CrudRepository<User>(context);
            _componentRepository = new CrudRepository<Models.Component>(context);
            _pendingOrderRepository = new CrudRepository<PendingOrder>(context);
        }
        public async Task<IEnumerable<Order>> LoadOrdersInfo()
        {
            return await _crudOrderRepository.GetAllAsync();
        }
        public async Task AddOrderItem(int prescriptionId, int medicationId, decimal amount, int registarId, DateTime? orderDate,
            string status, decimal? price, Models.Component? component, decimal? requiredAmount)
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
            if (status == ORDER_STATUS)
            {
                var pendingOrder = new PendingOrder
                {
                    OrderId = order.OrderId,
                    ComponentId = component.ComponentId,
                    RequiredAmount = requiredAmount
                };
                await _pendingOrderRepository.AddAsync(pendingOrder);
            }

        }
        public async Task DeleteOrderItem(Order orderToDelete)
        {
            await _crudOrderRepository.DeleteAsync(orderToDelete);
        }
        public async Task UpdateOrderItem(Order orderToUpdate, int newPrescriptionId, int newMedicationId, decimal newAmount, int newRegistratId,
            DateTime? newOrderDate, string newStatus, decimal? newPrice, Models.Component? component, decimal? amount)
        {
            orderToUpdate.PrescriptionId = newPrescriptionId;
            orderToUpdate.MedicationId = newMedicationId;
            orderToUpdate.Amount = newAmount;
            orderToUpdate.RegistrarId = newRegistratId;
            orderToUpdate.OrderDate = newOrderDate;
            statusBeforeUpdate = orderToUpdate.Status;
            orderToUpdate.Status = newStatus;
            orderToUpdate.Price = newPrice;
            await _crudOrderRepository.UpdateAsync(orderToUpdate);
            if (statusBeforeUpdate != ORDER_STATUS)
            {
                await CreatePendingOrderItem(newStatus, orderToUpdate,component,amount);
            }
            else 
            {
                await UpdatePendingItem(newStatus, orderToUpdate,component,amount);
            }
        }
        private async Task CreatePendingOrderItem(string status, Order order, Models.Component? component, decimal? amount)
        {
            if (status == ORDER_STATUS)
            {
                var pendingOrder = new PendingOrder
                {
                    OrderId = order.OrderId,
                    ComponentId = component.ComponentId,
                    RequiredAmount = amount
                };
                await _pendingOrderRepository.AddAsync(pendingOrder);
            }
            else
            {

                return;
            }
        }
        private async Task UpdatePendingItem(string status, Order order, Models.Component? component, decimal? amount)
        {
            if (status == ORDER_STATUS)
            {
                var pendingOrder = order.PendingOrders.First(c => c.OrderId == order.OrderId);
                pendingOrder.Component = component;
                pendingOrder.RequiredAmount = amount;
                await _pendingOrderRepository.UpdateAsync(pendingOrder);
            }
            else
            {
                if (order.PendingOrders.Any(c => c.OrderId == order.OrderId))
                {
                    var pendingOrder = order.PendingOrders.First(c => c.OrderId == order.OrderId);
                    await _pendingOrderRepository.DeleteAsync(pendingOrder);
                }
            }
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
        public async Task<IEnumerable<Models.Component>> LoadComponentInfo()
        {
            return await _componentRepository.GetAllAsync();
        }
    }
}
