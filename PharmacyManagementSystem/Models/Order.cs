using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PharmacyManagementSystem.Models
{
    public class Order
    {
        [Key]
        public int order_id { get; set; }

        //-----------------------------------------------------------------
        [Required]
        public int order_no { get; set; }

        //-----------------------------------------------------------------
        [Required]
        public decimal total { get; set; }

        //-----------------------------------------------------------------

        public User user { get; set; }
        public int UserId { get; set; }


        //-----------------------------------------------------------------
        public DateTime pickup_date { get; set; }
        [JsonIgnore]
        public List<OrderDetail> OrderDetails { get; set; }


        public Order() { }

    }
}