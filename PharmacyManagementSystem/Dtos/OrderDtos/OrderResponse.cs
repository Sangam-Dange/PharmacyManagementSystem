using PharmacyManagementSystem.Models;

namespace PharmacyManagementSystem.Dtos.OrderDtos
{
    public class OrderResponse
    {
        public Order order { get; set; }

        public List<OrderDetail> orderDetail { get; set; }
    }
}
