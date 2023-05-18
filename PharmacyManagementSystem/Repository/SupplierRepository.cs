using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmacyManagementSystem.Controllers.Dtos.SupplierDtos;
using PharmacyManagementSystem.Data;
using PharmacyManagementSystem.Interface;
using PharmacyManagementSystem.Models;

namespace PharmacyManagementSystem.Repository
{
    public class SupplierRepository : ISupplier
    {
        private readonly PharmacyManagementSystemContext _context;

        public SupplierRepository(PharmacyManagementSystemContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<SupplierDetails>>> GetSupplierDetail()
        {
            if (_context.SupplierDetail == null)
            {
                throw new Exception("Supplier DB is null");
            }
            return await _context.SupplierDetail.ToListAsync();
        }

        // GET: api/SupplierDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SupplierDetails>> GetSupplierDetailById(int id)
        {
            if (_context.SupplierDetail == null)
            {
                throw new Exception("Supplier DB is null");
            }
            var supplierDetails = await _context.SupplierDetail.FindAsync(id);

            if (supplierDetails == null)
            {
                throw new Exception("No supplier found");
            }

            return supplierDetails;
        }


        public async Task<SupplierDetails> PutSupplierDetails(int id, AddSupplierDto supplier)
        {

            SupplierDetails sp = await _context.SupplierDetail.FindAsync(id);

            if (sp == null)
            {
                throw new Exception("Supplier not found");
            }
            sp.supplier_address = supplier.supplier_address;
            sp.supplier_name = supplier.supplier_name;
            sp.supplier_phone = supplier.supplier_phone;
            sp.supplier_email = supplier.supplier_email;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException exception)
            {
                if (!SupplierDetailsExists(id))
                {
                    throw new Exception(exception.Message);
                }
                else
                {
                    throw new Exception("Bad request");
                }
            }

            return sp;
        }


        public async Task<SupplierDetails> PostSupplierDetails(AddSupplierDto supplier)
        {
            if (_context.SupplierDetail == null)
            {
                throw new Exception("Entity set 'PharmacyManagementSystemContext.SupplierDetail'  is null.");
            }

            SupplierDetails newSupplier = new SupplierDetails();
            newSupplier.supplier_address = supplier.supplier_address;
            newSupplier.supplier_name = supplier.supplier_name;
            newSupplier.supplier_phone = supplier.supplier_phone;
            newSupplier.supplier_email = supplier.supplier_email;

            _context.SupplierDetail.Add(newSupplier);
            await _context.SaveChangesAsync();

            return newSupplier;
        }


        public async Task<bool> DeleteSupplierDetails(int id)
        {
            if (_context.SupplierDetail == null)
            {
                throw new Exception("Entity set 'PharmacyManagementSystemContext.SupplierDetail'  is null.");
            }
            var supplierDetails = await _context.SupplierDetail.FindAsync(id);
            if (supplierDetails == null)
            {
                throw new Exception("Supplier not found");
            }

            _context.SupplierDetail.Remove(supplierDetails);
            await _context.SaveChangesAsync();

            return true;
        }

        public bool SupplierDetailsExists(int id)
        {
            return (_context.SupplierDetail?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
