using System;
using System.Collections.Generic;
using System.Text;

namespace RealWorldApp.Models
{
    public class SideDish
    {
        public int Id { get; set; }
        public int MainDishId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }
}
