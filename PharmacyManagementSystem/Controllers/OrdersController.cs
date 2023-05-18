using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagementSystem.Controllers.Dtos.OrderDtos;
using PharmacyManagementSystem.Interface;
using PharmacyManagementSystem.Models;

namespace PharmacyManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrder IOrder;

        public OrdersController(IOrder IOrder)
        {
            this.IOrder = IOrder;
        }

        // GET: api/Orders
        [HttpGet, Authorize(Roles = "Admin,Doctor")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrder()
        {

            return await IOrder.GetOrder();
        }

        // GET: api/Orders/5
        [HttpGet("{id}"), Authorize(Roles = "Admin,Doctor")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            return await IOrder.GetOrder(id);
        }

        // GET: api/Orders/5
        [HttpGet("GetOrderByUserId/{userId}"), Authorize(Roles = "Doctor")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrderByUserId(int userId)
        {
            return await IOrder.GetOrderByUserId(userId);
        }
        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}"), Authorize(Roles = "Doctor")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.order_id)
            {
                return BadRequest();
            }
            bool check = await IOrder.PutOrder(id, order);
            if (check)
            {
                return Ok();
            }
            return BadRequest();
        }

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost, Authorize(Roles = "Doctor")]
        public async Task<ActionResult<Order>> PostOrder(CreateOrderDto request)
        {
            Order newOrder = await IOrder.PostOrder(request);
            if (newOrder != null)
            {

                return CreatedAtAction("GetOrder", new { id = newOrder.order_id }, newOrder);
            }
            return BadRequest();
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}"), Authorize(Roles = "Doctor")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            bool check = await IOrder.DeleteOrder(id);
            if (check)
            {
                return Ok();
            }
            return BadRequest();
        }


    }
}
