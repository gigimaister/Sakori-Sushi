using System;
using System.Collections.Generic;
using System.Text;

namespace RealWorldApp.Models
{
    public class MainCourseProductDishes
    {
        public int Id { get; set; }
        public int MainDishesId { get; set; }
        public string Name { get; set; }       
        public MainDishes MainDishes { get; set; }
        public string ImageUrl { get; set; }
        public string FullImageUrl => $"{AppSettings.ApiUrl}/{ImageUrl}";
    }
}
