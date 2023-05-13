using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmacyManagementSystem.Controllers.Dtos.SupplierDtos;
using PharmacyManagementSystem.Data;
using PharmacyManagementSystem.Models;

namespace PharmacyManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierDetailsController : ControllerBase
    {
        private readonly PharmacyManagementSystemContext _context;

        public SupplierDetailsController(PharmacyManagementSystemContext context)
        {
            _context = context;
        }

        // GET: api/SupplierDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupplierDetails>>> GetSupplierDetail()
        {
            if (_context.SupplierDetail == null)
            {
                return NotFound();
            }
            return await _context.SupplierDetail.ToListAsync();
        }

        // GET: api/SupplierDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SupplierDetails>> GetSupplierDetails(int id)
        {
            if (_context.SupplierDetail == null)
            {
                return NotFound();
            }
            var supplierDetails = await _context.SupplierDetail.FindAsync(id);

            if (supplierDetails == null)
            {
                return NotFound();
            }

            return supplierDetails;
        }

        // PUT: api/SupplierDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSupplierDetails(int id, SupplierDetails supplierDetails)
        {
            if (id != supplierDetails.Id)
            {
                return BadRequest();
            }

            _context.Entry(supplierDetails).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SupplierDetailsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/SupplierDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SupplierDetails>> PostSupplierDetails(AddSupplier supplier)
        {
            if (_context.SupplierDetail == null)
            {
                return Problem("Entity set 'PharmacyManagementSystemContext.SupplierDetail'  is null.");
            }

            SupplierDetails newSupplier = new SupplierDetails();
            newSupplier.supplier_address = supplier.supplier_address;
            newSupplier.supplier_name = supplier.supplier_name;
            newSupplier.supplier_phone = supplier.supplier_phone;
            newSupplier.supplier_email = supplier.supplier_email;

            _context.SupplierDetail.Add(newSupplier);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSupplierDetails", new { id = newSupplier.Id }, newSupplier);
        }

        // DELETE: api/SupplierDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplierDetails(int id)
        {
            if (_context.SupplierDetail == null)
            {
                return NotFound();
            }
            var supplierDetails = await _context.SupplierDetail.FindAsync(id);
            if (supplierDetails == null)
            {
                return NotFound();
            }

            _context.SupplierDetail.Remove(supplierDetails);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SupplierDetailsExists(int id)
        {
            return (_context.SupplierDetail?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
