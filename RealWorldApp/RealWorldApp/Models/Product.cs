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
        public bool HasPaidSideDish { get; set; }
        public int categoryId { get; set; }
        public bool IsMeatSelect { get; set; }
        public bool IsFishSelect { get; set; }
        public bool IsVegSelect { get; set; }
        public int MaxMeatSelect { get; set; }
        public int MaxFishSelect { get; set; }
        public int MaxVegSelect { get; set; }
        public object imageArray { get; set; }

        // For Main Course
        public int MainCourseToProductId { get; set; }
        public  List<MainCourseToProduct> MainCourseToProduct { get; set; }
        public List<SideDish> SideDishList { get; set; }
        public List<PaidSideDish> PaidSideDishes { get; set; }
        public string FullImageUrl => $"{AppSettings.ApiUrl}/{imageUrl}";
        public string GetFullDetail()
        {
            string fullDeatil = $"{detail}\n{Constants.HebrewMessages.Menu_SideDishes}";
            if(SideDishList is null)  return detail;
            foreach (var sideDish in SideDishList)
            {
                fullDeatil += $"\n{sideDish.Name}";
            }
            return fullDeatil;
        }
        public string GetPaidSDFullDetail(string sideDishes)
        {
            string fullDeatil = sideDishes;
            if (PaidSideDishes is null) return fullDeatil;
            if (PaidSideDishes.Count == 0) return fullDeatil;
            fullDeatil += $"\n{Constants.HebrewMessages.Menu_PaidSideDishes}";
            foreach (var paidSideDish in PaidSideDishes)
            {
                fullDeatil += $"\n{paidSideDish.Name}  {paidSideDish.Price} ₪";
            }
            return fullDeatil;
        }

        // Get Number Of SideDishe Elements By Main Dish Id
        public int GetSideDishCount(int mainDish)
        {
            if (SideDishList is null) {
                SideDishList = new List<SideDish>();
                return 0; }
            var mainDishCounter = SideDishList.Where(x => x.MainDishId == mainDish).Count();
            return mainDishCounter;

        }
     
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

        // Product Detail Page Add To Cart Validator
        public List<string> ProductDetailValidator()
        {
            var validatorMessages = new List<string>();
            if (IsProductSelectable)
            {
                if (SideDishList is null) { validatorMessages.Add(TraslatedMessages.Alert_No_SideDish_Selected()); return validatorMessages; }
                
                if (IsMeatSelect)
                {
                    var maxMeat = SideDishList.Where(x => x.MainDishId == (int)MainDish.Meat).Count();
                    if (MaxMeatSelect != maxMeat) { validatorMessages.Add(TraslatedMessages.Alert_Product_Detail_Validator(MaxMeatSelect)); }
                }
                if (IsFishSelect)
                {
                    var maxFish = SideDishList.Where(x => x.MainDishId == (int)MainDish.Fish).Count();
                    if(MaxFishSelect != maxFish) { validatorMessages.Add(TraslatedMessages.Alert_Product_Detail_Validator(MaxFishSelect)); }
                }
                if (IsVegSelect)
                {
                    var maxVeg = SideDishList.Where(x => x.MainDishId == (int)MainDish.Veg).Count();
                    if (MaxVegSelect != maxVeg) { validatorMessages.Add(TraslatedMessages.Alert_Product_Detail_Validator(MaxVegSelect)); }
                }
            }
            return validatorMessages;
        }

        /// <summary>
        /// Check If PaidSideDish Is Empty
        /// </summary>
        /// <returns>bool true if not null and count > 0
        /// </returns>     
        public bool IsPaidSDishEmpty()
        {
            if (PaidSideDishes != null)
            {
                if (PaidSideDishes.Count > 0)
                {
                    return false;
                }               
            }
            return true;
        }

        /// <summary>
        /// Check If Main Course Is Empty
        /// </summary>
        /// <returns>True If Empty List</returns>
        public bool IsMainCourseIsEmpty()
        {
            if(MainCourseToProduct != null)
            {
                if(MainCourseToProduct.Count > 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
