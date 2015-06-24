using JobTestTelerikMvcApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobTestTelerikMvcApp.BUS
{
    public class WebDB
    {
        public static minhhoa_demoEntities entity;
        static WebDB()
        {
            entity = new minhhoa_demoEntities();
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
            
            List<RetailDTO> retail = entity.Retails.Select(s => new RetailDTO { ID = s.ID,Name=s.Name,FullAddress=s.Address }).ToList();
            return retail;
        }

        public static string getDis(long ID)
        {
            return entity.Distributors.Where(w => w.ID.Equals(ID)).Select(s => new DistributorDTO() {Name=s.Name }).FirstOrDefault().Name;
        }

        public static List<JobTestTelerikMvcApp.Models.Product> getProductList()
        {
            var list = entity.Products.Select(s => new JobTestTelerikMvcApp.Models.Product { GoodsID = s.ID, GoodsName = s.Name, Price = s.Price, WholesalePrice =s.Price}).ToList();
            //list.Add(new JobTestTelerikMvcApp.Models.Product() { GoodsID = 3, GoodsName = "Cocacola", Price = "10000" });
            return list;
        }

        public static List<JobTestTelerikMvcApp.Models.UnitDTO> getUnitList()
        {
            return entity.Types.Select(s => new JobTestTelerikMvcApp.Models.UnitDTO { UnitID = s.ID, UnitName = s.Name}).ToList();
        }

        public static string genNumerBill()
        {
           return entity.Database.SqlQuery<string>("exec minhhoa.sp_GenNumberBil").ElementAt(0).ToString();
        }
    }
}