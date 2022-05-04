using System;
using System.Collections.Generic;
using System.Text;

namespace RealWorldApp.Models
{
    public class MainCourseToProduct
    {
        public int Id { get; set; }   
        public int ProductId { get; set; }
        public double Price { get; set; }
        public int MainCourseProductDishesId { get; set; }
        public  MainCourseProductDishes MainCourseProductDishes { get; set; }
    }
}
