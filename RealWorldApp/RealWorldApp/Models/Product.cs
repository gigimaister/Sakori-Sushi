using System;
using System.Collections.Generic;
using System.Linq;
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

        // Get Number Of SideDishe Elements By Main Dish Id
        public int GetSideDishCount(int mainDish)
        {
            if (SideDishList is null) {
                SideDishList = new List<SideDish>();
                return 0; }
            var mainDishCounter = SideDishList.Where(x => x.MainDishId == mainDish).Count();
            return mainDishCounter;

        }

        // Get

        // Determine If We Can Add SideDish To SideDish List
        public bool IsAddToSideList(int mainDish)
        {
            var maxValue = GetMaxSideDishByMainId(mainDish);
            if (GetSideDishCount(mainDish) == maxValue) return true;
            return false;
        }

        // Get Max SideDishes By Main Dish Id
        public int GetMaxSideDishByMainId(int mainDish)
        {
            int maxValue = 0;
            switch (mainDish)
            {
                case (int)MainDish.Meat:
                    maxValue = MaxMeatSelect;
                    break;
                case (int)MainDish.Fish:
                    maxValue = MaxFishSelect;
                    break;
                case (int)MainDish.Veg:
                    maxValue = MaxVegSelect;
                    break;
            }
            return maxValue;
        }
    }
}
