using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagementSystem.Controllers.Dtos.OrderDtos;
using PharmacyManagementSystem.Interface;
using PharmacyManagementSystem.Models;

namespace PharmacyManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrugsController : ControllerBase
    {
        private readonly IDrug IDrug;

        public DrugsController(IDrug IDrug)
        {
            this.IDrug = IDrug;
        }

        // GET: api/Drugs
        // Access : Doctor , Admin 
        [HttpGet
            //, Authorize(Roles = "Admin,Doctor")
            ]
        public async Task<ActionResult<IEnumerable<Drug>>> GetDrug()
        {
            var drug = await IDrug.GetDrug();
            if (drug == null)
            {
                return NotFound();
            }
            return drug;
        }

        // GET: api/Drugs/5
        // Access : Doctor , Admin 
        [HttpGet("{id}")
            //, Authorize(Roles = "Admin,Doctor")
            ]
        public async Task<ActionResult<Drug>> GetDrug(int id)
        {
            var drug = await IDrug.GetDrugById(id);
            if (drug == null)
            {
                return NotFound();
            }
            return Ok(drug);
        }

        // PUT: api/Drugs/5
        // Access : Admin 
        [HttpPut("{id}")
            //, Authorize(Roles = "Admin")
            ]
        public async Task<IActionResult> PutDrug(int id, Drug drug)
        {
            if (id != drug.drug_id)
            {
                return BadRequest();
            }

            bool check = await IDrug.PutDrugById(id, drug);
            if (check == false)
            {
                return BadRequest();
            }
            return Ok();

        }

        // POST: api/Drugs
        // Access : Admin 
        [HttpPost
            //, Authorize(Roles = "Admin")
            ]
        public async Task<ActionResult<Drug>> PostDrug(CreateDrugDto drug)
        {

            if (drug == null)
            {
                return BadRequest();
            }

            var newDrug = await IDrug.PostDrug(drug);

            if (newDrug == null)
            {
                return BadRequest();
            }

            return CreatedAtAction("GetDrug", new { id = newDrug.drug_id }, newDrug);
        }

        // DELETE: api/Drugs/5
        // Access : Admin 
        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteDrug(int id)
        {
            bool check = await IDrug.DeleteDrugById(id);
            if (check)
            {
                return Ok();
            }
            return BadRequest();
        }


        [HttpGet("GetDrugBySupplierId/{supplierId}")
            //, Authorize(Roles = "Admin")
            ]
        public async Task<ActionResult<IEnumerable<Drug>>> GetDrugBySupplierId(int supplierId)
        {
            return await IDrug.GetDrugBySupplierId(supplierId);
        }

    }
}
