using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace Buy_And_Sell.Models

{
    public class Buyer
    {
        [Key]
        public int BuyerId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage = "Phone NUmber is required!")]
        public int PhoneNumber { get; set; }

    }
}
