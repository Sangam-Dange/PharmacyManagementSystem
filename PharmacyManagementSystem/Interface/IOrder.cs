using Microsoft.AspNetCore.Mvc;
using PharmacyManagementSystem.Controllers.Dtos.OrderDtos;
using PharmacyManagementSystem.Models;

namespace PharmacyManagementSystem.Interface
{
    public interface IOrder
    {
        Task<ActionResult<IEnumerable<Order>>> GetOrder();
        Task<ActionResult<Order>> GetOrder(int id);
        Task<ActionResult<IEnumerable<Order>>> GetOrderByUserId(int userId);
        Task<bool> PutOrder(int id, Order order);
        Task<Order> PostOrder(CreateOrderDto request);
        Task<bool> DeleteOrder(int id);
        bool OrderExists(int id);

    }
}
