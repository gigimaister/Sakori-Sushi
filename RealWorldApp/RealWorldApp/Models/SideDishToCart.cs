using System;
using System.Collections.Generic;
using System.Text;

namespace RealWorldApp.Models
{
    public class SideDishToCart
    {
        public int CartId { get; set; }
        public int SideDishId { get; set; }
        public bool IsChargeExtra { get; set; }
        public SideDish SideDish { get; set; }
        public PaidSideDish PaidSideDish { get; set; }
    }
}
