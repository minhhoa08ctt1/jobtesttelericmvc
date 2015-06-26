using JobTestTelerikMvcApp.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;

namespace JobTestTelerikMvcApp.BUS
{
    public class WebDB
    {
        public WebDB()
        {
        }

        public static minhhoa_demoEntities getDB()
        {
            return new minhhoa_demoEntities();
        }
        public static List<MoneyType> getMTList()
        {
            List<MoneyType> list = new List<MoneyType>();
            MoneyType mt = new MoneyType();
            mt.Name = "VNĐ";
            mt.ID = "1";
            list.Add(mt);
            MoneyType mt2 = new MoneyType();
            mt2.Name = "USD";
            mt2.ID = "2";
            list.Add(mt2);
            return list;
        }
        public static List<RetailDTO> getRetaiList()
        {
            using (minhhoa_demoEntities db = getDB())
            {
                List<RetailDTO> retail = db.Retails.Select(s => new RetailDTO { ID = s.ID, Name = s.Name, FullAddress = s.Address }).ToList();
                return retail;
            }
        }

        public static string getDis(long ID)
        {
            using (minhhoa_demoEntities db = getDB())
            {
                return db.Distributors.Where(w => w.ID.Equals(ID)).Select(s => new DistributorDTO() { Name = s.Name }).FirstOrDefault().Name;
            }
        }

        public static List<JobTestTelerikMvcApp.Models.Product> getProductList()
        {
            using (minhhoa_demoEntities db = getDB())
            {
                var list = db.Products.Select(s => new JobTestTelerikMvcApp.Models.Product { GoodsID = s.ID, GoodsName = s.Name, Price = s.Price, WholesalePrice = s.Price }).ToList();
                //list.Add(new JobTestTelerikMvcApp.Models.Product() { GoodsID = 3, GoodsName = "Cocacola", Price = "10000" });
                return list;
            }
        }

        public static List<JobTestTelerikMvcApp.Models.UnitDTO> getUnitList()
        {
            using (minhhoa_demoEntities db = getDB())
            {
                return db.Types.Select(s => new JobTestTelerikMvcApp.Models.UnitDTO { UnitID = s.ID, UnitName = s.Name }).ToList();
            }
        }

        public static string genNumerBill()
        {
            using (minhhoa_demoEntities db = getDB())
            {
                string result = db.Database.SqlQuery<string>("exec minhhoa.sp_GenNumberBil").ElementAt(0).ToString();
                return result;
            }
        }

        public static List<SaleGridDTO> getGridData(long ID)
        {
            using (minhhoa_demoEntities db = getDB())
            {
                List<SaleGridDTO> list = new List<SaleGridDTO>();
                list = db.Bill_Product.Where(w => w.BillID == ID).Select(s => new SaleGridDTO { GoodsID = s.ProductID, Quantity = s.Quantity.Value, UnitID = s.TypeID.Value, OfferPrice = s.Product.Price, OfferAmount = s.Product.Price * s.Quantity.Value }).ToList();
                return list;
            }
        }

        public static bool Has(object obj, string propertyName)
        {
            var dynamic = obj as DynamicObject;
            if (dynamic == null) return false;
            return dynamic.GetDynamicMemberNames().Contains(propertyName);
        }

    }
}