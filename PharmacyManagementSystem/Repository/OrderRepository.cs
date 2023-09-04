using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmacyManagementSystem.Data;
using PharmacyManagementSystem.Dtos.OrderDetailDtos;
using PharmacyManagementSystem.Dtos.OrderDtos;
using PharmacyManagementSystem.Interface;
using PharmacyManagementSystem.Models;
using System.Security.Claims;

namespace PharmacyManagementSystem.Repository
{
    public class OrderRepository : IOrder
    {
        private readonly PharmacyManagementSystemContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderRepository(PharmacyManagementSystemContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;

            _httpContextAccessor = httpContextAccessor;


        }


        public async Task<ActionResult<IEnumerable<Order>>> GetAllOrders()
        {
            if (_context.Order == null)
            {
                throw new Exception("Entity set 'PharmacyManagementSystemContext.Order'  is null.");
            }

            return await _context.Order.Include(o => o.user).ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<Order>>> GetMyOrders()
        {
            if (_context.Order == null)
            {
                throw new Exception("Entity set 'PharmacyManagementSystemContext.Order'  is null.");
            }

            var userId = _httpContextAccessor?.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                throw new Exception("User id not found" + ClaimTypes.NameIdentifier);
            }

            return await _context.Order.Where(x => x.UserId == int.Parse(userId)).Include(o => o.user).ToListAsync();
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



        public async Task<Order> PlaceOrder(PlaceOrderDto placeOrderDto)
        {
            try
            {
                var request = placeOrderDto.createOrderDto;
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
                    totalItems = request.totalItems,
                    UserId = request.UserId,
                    order_date = request.order_date,

                };
                _context.Order.Add(newOrder);
                await _context.SaveChangesAsync();

                if (newOrder == null)
                {
                    throw new Exception("Error");
                }

                List<OrderDetailsDto> od = placeOrderDto.orderDetails;

                foreach (OrderDetailsDto o in od)
                {
                    var currOrder = await _context.Drug.Where(x => x.drug_id == o.DrugId).FirstOrDefaultAsync();

                    if (_context.Drug == null)
                    {
                        throw new Exception("Entity set 'PharmacyManagementSystemContext.Drug'  is null.");
                    }

                    if (currOrder == null)
                    {
                        throw new Exception("Drug of Id " + o.DrugId + " is out of stock , Please visit the website ");
                    }
                    OrderDetail newOrderItem = new OrderDetail();
                    newOrderItem.drug_quantity = o.drug_quantity;
                    newOrderItem.DrugId = o.DrugId;
                    newOrderItem.OrderId = newOrder.order_id;
                    newOrderItem.sub_total = o.sub_total;

                    // subtracting ordered quantity from available quantity;
                    currOrder.quantity = currOrder.quantity - o.drug_quantity;

                    if (_context.OrderDetail == null)
                    {
                        throw new Exception("Entity set 'PharmacyManagementSystemContext.OrderDetail'  is null.");
                    }

                    await _context.OrderDetail.AddAsync(newOrderItem);


                }

                await _context.SaveChangesAsync();
                //EmailDto email = new EmailDto() { Body = $"Your Order Confirmation-[{newOrder.order_no}]", Subject = "", To = ClaimTypes.Email };
                //await this._emailService.SendEmail(email);
                return newOrder;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        public async Task<OrderResponse> getOrderDetailsByOrderId(int id)
        {
            if (_context.Order == null)
            {
                throw new Exception("Entity set 'PharmacyManagementSystemContext.Order'  is null.");
            }

            Order? order = await _context.Order.FindAsync(id);

            if (order == null)
            {
                throw new Exception("Order not found");
            }
            if (_context.OrderDetail == null)
            {
                throw new Exception("Entity set 'PharmacyManagementSystemContext.OrderDetail'  is null.");
            }
            var orderDetails = await _context.OrderDetail.Where(x => x.OrderId == order.order_id).Include(x => x.drug).ToListAsync();


            return new OrderResponse { order = order, orderDetail = orderDetails };

        }


        public async Task<Order> updateOrderStatus(int id)
        {
            try
            {

                Order order = await _context.Order.FindAsync(id);

                order.pickup_date = DateTime.Now;
                await _context.SaveChangesAsync();
                return order;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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
