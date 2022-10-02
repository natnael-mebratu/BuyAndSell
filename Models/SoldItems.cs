using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Buy_And_Sell.Models
{
    public class SoldItems
    {
        [Key]
        public int SoldId { get; set; }
        public virtual Buyer Buyer { get; set; }
        public virtual Seller Seller { get; set; }
        public virtual Vehicle Vehicle { get; set; }
        public virtual House House { get; set; }
    }
}
