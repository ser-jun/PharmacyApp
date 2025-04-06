using PharmacyApp.Models;

namespace PharmacyApp.Repositories.Interfaces
{
    public interface ISupplyRequestRepository
    {
        Task<IEnumerable<SupplyRequest>> LoadSupplyRequest();
        Task DeleteSupplyRequestItem(SupplyRequest itemToDelete);
        Task UpdateSupplyRequestItem(SupplyRequest itemToUpdate, decimal? newAmount, DateTime? newTime, string newStatus);
    }
}
