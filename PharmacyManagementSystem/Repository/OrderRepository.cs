using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmacyManagementSystem.Controllers.Dtos.OrderDtos;
using PharmacyManagementSystem.Data;
using PharmacyManagementSystem.Interface;
using PharmacyManagementSystem.Models;

namespace PharmacyManagementSystem.Repository
{
    public class OrderRepository : IOrder
    {
        private readonly PharmacyManagementSystemContext _context;

        public OrderRepository(PharmacyManagementSystemContext context)
        {
            _context = context;
        }


        public async Task<ActionResult<IEnumerable<Order>>> GetOrder()
        {
            if (_context.Order == null)
            {
                throw new Exception("Entity set 'PharmacyManagementSystemContext.Order'  is null.");
            }
            return await _context.Order.Include(o => o.user).ToListAsync();
        }


        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            if (_context.Order == null)
            {
                throw new Exception("Entity set 'PharmacyManagementSystemContext.Order'  is null.");
            }
            var order = await _context.Order.FindAsync(id);

            if (order == null)
            {
                throw new Exception("Order not found");
            }

            return order;
        }


        public async Task<ActionResult<IEnumerable<Order>>> GetOrderByUserId(int userId)
        {
            if (_context.Order == null)
            {
                throw new Exception("Entity set 'PharmacyManagementSystemContext.Order'  is null.");
            }
            var order = await _context.Order.Where(o => o.UserId == userId).ToListAsync();

            if (order == null)
            {
                throw new Exception("Order not found");
            }

            return order;
        }

        public async Task<bool> PutOrder(int id, Order order)
        {


            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    throw new Exception("Order Id doesn't exist");
                }
                else
                {
                    return false;
                }
            }

            return true;
        }


        public async Task<Order> PostOrder(CreateOrderDto request)
        {
            if (_context.Order == null)
            {
                throw new Exception("Entity set 'PharmacyManagementSystemContext.Order'  is null.");
            }

            Random rnd = new Random();
            int num = rnd.Next();
            var newOrder = new Order
            {
                order_no = num,
                total = request.total,
                UserId = request.UserId,
                pickup_date = request.pickup_date,
            };
            _context.Order.Add(newOrder);
            await _context.SaveChangesAsync();

            return newOrder;
        }


        public async Task<bool> DeleteOrder(int id)
        {
            if (_context.Order == null)
            {
                throw new Exception("Entity set 'PharmacyManagementSystemContext.Order'  is null.");
            }
            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                throw new Exception("Order not found");
            }

            _context.Order.Remove(order);
            await _context.SaveChangesAsync();

            return true;
        }

        public bool OrderExists(int id)
        {
            return (_context.Order?.Any(e => e.order_id == id)).GetValueOrDefault();
        }
    }
}
