﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PharmacyManagementSystem.Controllers.Dtos.OrderDtos
{
    public class CreateDrugDto
    {
        //-----------------------------------------------------------------
        [Required(ErrorMessage = "Please enter drug name")]
        [StringLength(50)]
        [DisplayName("Drug Name")]
        public string drug_name { get; set; }

        //-----------------------------------------------------------------
        [Required(ErrorMessage = "Please enter drug price")]
        [DisplayName("Price")]
        public decimal price { get; set; }

        //-----------------------------------------------------------------
        [Required(ErrorMessage = "Please enter batch id")]
        [StringLength(10)]
        [DisplayName("Batch Id")]
        public string batch_id { get; set; }

        //-----------------------------------------------------------------
        [Required(ErrorMessage = "Please enter total quantity of drug available")]
        [DisplayName("Quantity")]
        public int quantity { get; set; }

        //-----------------------------------------------------------------
        [Required(ErrorMessage = "Please enter expiry date of drug")]
        [DisplayName("Expiry Date")]
        public DateTime expiry_date { get; set; }

        public int SupplierDetailId { get; set; }
    }
}
