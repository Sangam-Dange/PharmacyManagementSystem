using Microsoft.AspNetCore.Mvc;
using PharmacyManagementSystem.Controllers.Dtos.OrderDtos;
using PharmacyManagementSystem.Models;

namespace PharmacyManagementSystem.Interface
{
    public interface IDrug
    {
        Task<ActionResult<IEnumerable<Drug>>> GetDrug();
        Task<ActionResult<Drug>> GetDrugById(int id);
        Task<bool> PutDrugById(int id, Drug drug);
        Task<Drug> PostDrug(CreateDrugDto drug);
        Task<ActionResult<IEnumerable<Drug>>> GetDrugBySupplierId(int supplierId);
        Task<bool> DeleteDrugById(int id);
        bool DrugExists(int id);
    }
}
