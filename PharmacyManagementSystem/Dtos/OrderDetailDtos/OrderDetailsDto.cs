using Microsoft.Build.Framework;

namespace PharmacyManagementSystem.Dtos.OrderDetailDtos
{
    public class OrderDetailsDto
    {
        [Required]
        public int DrugId { get; set; }

        [Required]
        public int drug_quantity { get; set; }

        [Required]
        public decimal sub_total { get; set; }
    }
}
