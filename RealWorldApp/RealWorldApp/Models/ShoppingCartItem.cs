using System.Collections.Generic;

namespace RealWorldApp.Models
{
    public class ShoppingCartItem
    {
        public int id { get; set; }
        public double price { get; set; }
        public double totalAmount { get; set; }
        public int qty { get; set; }
        public string productName { get; set; }
        public List<SideDishToCart> SideDishToCarts { get; set; }
        public Product Product { get; set; }

        public string SideDishesDescription => GetSideDishesDescription();

        public string GetSideDishesDescription()
        {
            var sDtoCartDescription = "";

            if (SideDishToCarts == null || SideDishToCarts.Count == 0)
            {
                return sDtoCartDescription;               
            }
            sDtoCartDescription += $"{Constants.HebrewMessages.Menu_SideDishes}";

            foreach (var sDtoCart in SideDishToCarts)
            {
                sDtoCartDescription += $"\n{sDtoCart.SideDish.Name}";
            }
            return sDtoCartDescription;
        }

    }
}
