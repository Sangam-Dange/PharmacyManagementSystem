using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmacyManagementSystem.Data;
using PharmacyManagementSystem.Models;

namespace PharmacyManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly PharmacyManagementSystemContext _context;

        public OrderDetailsController(PharmacyManagementSystemContext context)
        {
            _context = context;
        }

        // GET: api/OrderDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDetail>>> GetOrderDetail()
        {
            try
            {

                if (_context.OrderDetail == null)
                {
                    return NotFound();
                }
                return await _context.OrderDetail.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: api/OrderDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetail>> GetOrderDetail(int id)
        {
            try
            {

                if (_context.OrderDetail == null)
                {
                    return NotFound();
                }
                var orderDetail = await _context.OrderDetail.FindAsync(id);

                if (orderDetail == null)
                {
                    return NotFound();
                }
                return orderDetail;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // PUT: api/OrderDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderDetail(int id, OrderDetail orderDetail)
        {
            try
            {

                if (id != orderDetail.Id)
                {
                    return BadRequest();
                }

                _context.Entry(orderDetail).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderDetailExists(id))
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // POST: api/OrderDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderDetail>> PostOrderDetail(OrderDetail orderDetail)
        {
            try
            {

                if (_context.OrderDetail == null)
                {
                    return Problem("Entity set 'PharmacyManagementSystemContext.OrderDetail'  is null.");
                }
                _context.OrderDetail.Add(orderDetail);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetOrderDetail", new { id = orderDetail.Id }, orderDetail);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }




        // DELETE: api/OrderDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderDetail(int id)
        {
            try
            {

                if (_context.OrderDetail == null)
                {
                    return NotFound();
                }
                var orderDetail = await _context.OrderDetail.FindAsync(id);
                if (orderDetail == null)
                {
                    return NotFound();
                }

                _context.OrderDetail.Remove(orderDetail);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private bool OrderDetailExists(int id)
        {
            return (_context.OrderDetail?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
