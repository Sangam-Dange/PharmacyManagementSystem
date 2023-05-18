using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace PharmacyManagementSystem.Models
{
    public class SupplierDetails
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "Please enter supplier name"), MinLength(3), MaxLength(50)]
        public string supplier_name { get; set; }

        [Required(ErrorMessage = "Please enter supplier email id")]
        [DataType(DataType.EmailAddress)]
        public string supplier_email { get; set; }


        [Required(ErrorMessage = "Please enter supplier address"), MinLength(3), MaxLength(250)]
        public string supplier_address { get; set; }


        [Required(ErrorMessage = "Please enter contact number")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Supplier Contact Number")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string supplier_phone { get; set; }

        [JsonIgnore]
        public IEnumerable<Drug>? drug { get; set; }
    }
}
