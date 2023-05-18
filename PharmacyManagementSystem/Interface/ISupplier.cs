using Microsoft.AspNetCore.Mvc;
using PharmacyManagementSystem.Controllers.Dtos.SupplierDtos;
using PharmacyManagementSystem.Models;

namespace PharmacyManagementSystem.Interface
{
    public interface ISupplier
    {
        Task<ActionResult<IEnumerable<SupplierDetails>>> GetSupplierDetail();
        Task<ActionResult<SupplierDetails>> GetSupplierDetailById(int id);
        Task<SupplierDetails> PutSupplierDetails(int id, AddSupplierDto supplier);
        Task<SupplierDetails> PostSupplierDetails(AddSupplierDto supplier);
        Task<bool> DeleteSupplierDetails(int id);
        bool SupplierDetailsExists(int id);
    }
}
