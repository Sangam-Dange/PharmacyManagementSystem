namespace PharmacyManagementSystem.Controllers.Dtos.OrderDtos
{
    public class CreateOrderDto
    {



        //-----------------------------------------------------------------

        public decimal total { get; set; }

        //-----------------------------------------------------------------

        public int UserId { get; set; }


        //-----------------------------------------------------------------
        public DateTime pickup_date { get; set; }

    }
}
