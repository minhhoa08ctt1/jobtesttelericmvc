using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobTestTelerikMvcApp.Models
{
    public class Product
    {
        public long GoodsID { get; set; }
        public string GoodsName { get; set; }
        public double Price { get; set; }
        public double WholesalePrice { get; set; }
    }
}