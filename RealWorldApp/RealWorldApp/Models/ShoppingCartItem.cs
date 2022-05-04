using System.Collections.Generic;
using System.Linq;

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
        public string AllSideDishesDescription => GetPaidSideDishesDescription();
        public string CombineSdishDescription => $"{SideDishesDescription}{AllSideDishesDescription}";

        // Return SideDishes Description
        public string GetSideDishesDescription()
        {
            var sDtoCartDescription = "";

            if (SideDishToCarts == null || SideDishToCarts.Count == 0)
            {
                return sDtoCartDescription;               
            }
            // If We Have FREE SideDish Only!
            var paidSideDishes = SideDishToCarts.Any(x => !x.IsChargeExtra);
            if (paidSideDishes)
            {
                sDtoCartDescription += $"{Constants.HebrewMessages.Menu_SideDishes}";

                foreach (var sDtoCart in SideDishToCarts)
                {
                    if (!sDtoCart.IsChargeExtra)
                    {
                        sDtoCartDescription += $"\n{sDtoCart.SideDish.Name}";
                    }                   
                }
            }         
            return sDtoCartDescription;
        }
        // Return Paid SideDishes Description
        public string GetPaidSideDishesDescription()
        {
            var sDtoCartDescription = "";

            if (SideDishToCarts == null || SideDishToCarts.Count == 0)
            {
                return sDtoCartDescription;
            }
            // If We Have PAID SideDishes Only
            var paidSideDishes = SideDishToCarts.Any(x => x.IsChargeExtra);
            if (paidSideDishes)
            {
                sDtoCartDescription += $"\n{Constants.HebrewMessages.Menu_PaidSideDishes}";

                foreach (var sDtoCart in SideDishToCarts)
                {
                    if (sDtoCart.IsChargeExtra)
                    {
                        sDtoCartDescription += $"\n{sDtoCart.SideDish.Name}  {sDtoCart.SideDish.Price} ₪";
                    }
                }
            }         
            return sDtoCartDescription;
        }

    }
}
