using System.Collections.Generic;

namespace RealWorldApp.Models
{
    public class AddToCart
    {
        public string Price { get; set; }
        public string Qty { get; set; }
        public string TotalAmount { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public List<SideDish> SideDishes { get; set; }
    }
}
