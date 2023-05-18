using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmacyManagementSystem.Controllers.Dtos.OrderDtos;
using PharmacyManagementSystem.Data;
using PharmacyManagementSystem.Interface;
using PharmacyManagementSystem.Models;

namespace PharmacyManagementSystem.Repository
{
    public class DrugRepository : IDrug
    {

        private readonly PharmacyManagementSystemContext _context;


        public DrugRepository(PharmacyManagementSystemContext context)
        {
            _context = context;
        }


        public async Task<ActionResult<IEnumerable<Drug>>> GetDrug()
        {
            return await _context.Drug.Include(x => x.SupplierDetail).ToListAsync();
        }

        public async Task<ActionResult<Drug>> GetDrugById(int id)
        {

            var drug = await _context.Drug.FindAsync(id);
            return drug;
        }

        public async Task<bool> PutDrugById(int id, Drug drug)
        {
            _context.Entry(drug).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }

            return true;
        }

        public async Task<Drug> PostDrug(CreateDrugDto drug)
        {


            Drug newDrug = new Drug();
            newDrug.drug_name = drug.drug_name;
            newDrug.expiry_date = drug.expiry_date;
            newDrug.quantity = drug.quantity;
            newDrug.price = drug.price;
            newDrug.batch_id = drug.batch_id;

            newDrug.SupplierDetailId = drug.SupplierDetailId;
            _context.Drug.Add(newDrug);

            await _context.SaveChangesAsync();

            return newDrug;
        }

        public async Task<ActionResult<IEnumerable<Drug>>> GetDrugBySupplierId(int supplierId)
        {
            SupplierDetails sd = await _context.SupplierDetail.FindAsync(supplierId);

            if (sd == null)
            {
                throw new Exception("Supplier not found");
            }

            List<Drug> drug = await _context.Drug.Where(x => x.SupplierDetailId == supplierId).ToListAsync();

            if (drug == null)
            {
                throw new Exception("Drug not found");
            }

            return drug;
        }

        public async Task<bool> DeleteDrugById(int id)
        {
            if (_context.Drug == null)
            {
                throw new Exception("Drug table is emty");
            }
            var drug = await _context.Drug.FindAsync(id);
            if (drug == null)
            {
                throw new Exception("Drug not found");
            }

            _context.Drug.Remove(drug);
            await _context.SaveChangesAsync();

            return true;
        }

        public bool DrugExists(int id)
        {
            return (_context.Drug?.Any(e => e.drug_id == id)).GetValueOrDefault();
        }
    }
}
