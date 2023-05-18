using Microsoft.AspNetCore.Mvc;
using PharmacyManagementSystem.Controllers.Dtos.SupplierDtos;
using PharmacyManagementSystem.Interface;
using PharmacyManagementSystem.Models;

namespace PharmacyManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierDetailsController : ControllerBase
    {
        private readonly ISupplier ISupplier;

        public SupplierDetailsController(ISupplier ISupplier)
        {
            this.ISupplier = ISupplier;
        }



        // GET: api/SupplierDetails
        [HttpGet
            //, Authorize(Roles = "Admin")
            ]
        public async Task<ActionResult<IEnumerable<SupplierDetails>>> GetSupplierDetail()
        {
            return await ISupplier.GetSupplierDetail();
        }

        // GET: api/SupplierDetails/5
        [HttpGet("{id}")
            //,Authorize(Roles = "Admin")
            ]
        public async Task<ActionResult<SupplierDetails>> GetSupplierDetails(int id)
        {

            return await ISupplier.GetSupplierDetailById(id);
        }

        // PUT: api/SupplierDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")
            //, Authorize(Roles = "Admin")
            ]
        public async Task<IActionResult> PutSupplierDetails(int id, AddSupplierDto supplier)
        {

            var updatedSp = await ISupplier.PutSupplierDetails(id, supplier);

            if (updatedSp != null)
            {
                return Ok(updatedSp);
            }

            return BadRequest();
        }

        // POST: api/SupplierDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost
            //, Authorize(Roles = "Admin")
            ]
        public async Task<ActionResult<SupplierDetails>> PostSupplierDetails(AddSupplierDto supplier)
        {
            SupplierDetails newSupplier = await ISupplier.PostSupplierDetails(supplier);

            if (newSupplier != null)
            {
                return CreatedAtAction("GetSupplierDetails", new { id = newSupplier.Id }, newSupplier);
            }

            return BadRequest();

        }

        // DELETE: api/SupplierDetails/5
        [HttpDelete("{id}")
            //, Authorize(Roles = "Admin")
            ]
        public async Task<IActionResult> DeleteSupplierDetails(int id)
        {
            bool check = await ISupplier.DeleteSupplierDetails(id);

            if (check)
            {
                return Ok();
            }

            return BadRequest();
        }


    }
}
