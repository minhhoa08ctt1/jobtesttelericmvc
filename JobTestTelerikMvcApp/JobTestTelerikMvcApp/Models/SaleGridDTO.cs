using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobTestTelerikMvcApp.Models
{
    public class SaleGridDTO
    {
        public string ID { get; set; }
        public string AutoID { get; set; }
        [UIHint("GoodsIdEditor")]
        public long GoodsID { get; set; }
          [UIHint("UnitIdEditor")]
        public string UnitID { get; set; }
        public double Quantity { get; set; }
        public double OfferAmount { get; set; }
        public double OfferPrice { get; set; }     
    }
}