namespace PharmacyManagementSystem.Controllers.Dtos.OrderDtos
{
    public class CreateOrderDto
    {



        //-----------------------------------------------------------------

        public decimal total { get; set; }

        //-----------------------------------------------------------------
        public decimal totalItems { get; set; }
        public int UserId { get; set; }
        public string email { get; set; }
        public DateTime order_date { get; set; }

        //-----------------------------------------------------------------
        public DateTime pickup_date { get; set; }

    }
}
