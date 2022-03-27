using System;
using System.Collections.Generic;
using System.Text;

namespace RealWorldApp.Models
{
    public class Product
    {
        public int id { get; set; }
        public string name { get; set; }
        public string detail { get; set; }
        public string imageUrl { get; set; }
        public double price { get; set; }
        public bool isPopularProduct { get; set; }
        public bool IsProductSelectable { get; set; }
        public int categoryId { get; set; }
        public bool IsMeatSelect { get; set; }
        public bool IsFishSelect { get; set; }
        public bool IsVegSelect { get; set; }
        public int MaxMeatSelect { get; set; }
        public int MaxFishSelect { get; set; }
        public int MaxVegSelect { get; set; }
        public object imageArray { get; set; }
        public List<SideDish> SideDishList { get; set; }
        public string FullImageUrl => $"{AppSettings.ApiUrl}/{imageUrl}";

    }
}
