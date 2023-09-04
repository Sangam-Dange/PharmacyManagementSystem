using System.ComponentModel.DataAnnotations;

namespace PharmacyManagementSystem.Models
{
    public class UserAddress
    {
        [Key]
        public int AddressId { get; set; }

        [Required]
        [MaxLength(100)]
        public string StreetAddress { get; set; }

        [MaxLength(50)]
        public string City { get; set; }

        [MaxLength(50)]
        public string State { get; set; }

        [Required]
        [MaxLength(10)]
        public string PostalCode { get; set; }

        [MaxLength(100)]
        public string Country { get; set; }

        public User user { get; set; }
        public int UserId { get; set; }
    }
}
