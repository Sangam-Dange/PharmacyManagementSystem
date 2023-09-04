using PharmacyManagementSystem.Controllers.Dtos.OrderDtos;
using PharmacyManagementSystem.Dtos.OrderDetailDtos;


namespace PharmacyManagementSystem.Dtos.OrderDtos
{
    public class PlaceOrderDto
    {
        public CreateOrderDto createOrderDto { get; set; }

        public List<OrderDetailsDto> orderDetails { get; set; }
        public PlaceOrderDto() { }
    }
}
