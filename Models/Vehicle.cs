using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Buy_And_Sell.Models
{
    public class Vehicle
    {

        [Key]
        public int VehicleId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public DateTime Year { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public string Discription { get; set; }
        [Required]
        [DisplayName("Upload File")]
        public string Image { get; set; }
        [Display(Name = "Owner Name")]
        public string SellerName { get; set; }

    }
}
