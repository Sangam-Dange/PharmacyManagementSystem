using Microsoft.AspNetCore.Mvc;
using PharmacyManagementSystem.Dtos.OrderDtos;
using PharmacyManagementSystem.Models;

namespace PharmacyManagementSystem.Interface
{
    public interface IOrder
    {
        Task<ActionResult<IEnumerable<Order>>> GetAllOrders();

        Task<ActionResult<IEnumerable<Order>>> GetMyOrders();
        Task<ActionResult<Order>> GetOrder(int id);
        Task<ActionResult<IEnumerable<Order>>> GetOrderByUserId(int userId);
        Task<Order> PlaceOrder(PlaceOrderDto placeOrderDto);

        Task<OrderResponse> getOrderDetailsByOrderId(int id);

        Task<Order> updateOrderStatus(int id);
        Task<bool> DeleteOrder(int id);
        bool OrderExists(int id);
        //Task<bool> PutOrder(int id, Order order);
        //Task<Order> PostOrder(CreateOrderDto request);

    }
}
