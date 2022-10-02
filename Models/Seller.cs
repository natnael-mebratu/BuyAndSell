using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace Buy_And_Sell.Models
{
    public class Seller
    {
        [Key]
        public int SellerId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage = "Phone NUmber is required!")]
        public int PhoneNumber { get; set; }
    }
}
