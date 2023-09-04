using Microsoft.AspNetCore.Authorization;
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
        [HttpGet, Authorize(Roles = "Admin")
            ]
        public async Task<ActionResult<IEnumerable<SupplierDetails>>> GetSupplierDetail()
        {
            try
            {

                return await ISupplier.GetSupplierDetail();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: api/SupplierDetails/5
        [HttpGet("{id}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<SupplierDetails>> GetSupplierDetails(int id)
        {
            try
            {

                return await ISupplier.GetSupplierDetailById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        // PUT: api/SupplierDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}"), Authorize(Roles = "Admin")
            ]
        public async Task<IActionResult> PutSupplierDetails(int id, AddSupplierDto supplier)
        {
            try
            {

                var updatedSp = await ISupplier.PutSupplierDetails(id, supplier);

                if (updatedSp != null)
                {
                    return Ok(updatedSp);
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // POST: api/SupplierDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost, Authorize(Roles = "Admin")
            ]
        public async Task<ActionResult<SupplierDetails>> PostSupplierDetails(AddSupplierDto supplier)
        {
            try
            {

                SupplierDetails newSupplier = await ISupplier.PostSupplierDetails(supplier);

                if (newSupplier != null)
                {
                    return CreatedAtAction("GetSupplierDetails", new { id = newSupplier.Id }, newSupplier);
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        // DELETE: api/SupplierDetails/5
        [HttpDelete("{id}"), Authorize(Roles = "Admin")
            ]
        public async Task<IActionResult> DeleteSupplierDetails(int id)
        {
            try
            {
                bool check = await ISupplier.DeleteSupplierDetails(id);

                if (check)
                {
                    return Ok();
                }

                return BadRequest();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
