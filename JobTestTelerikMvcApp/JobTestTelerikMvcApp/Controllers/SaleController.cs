using JobTestTelerikMvcApp.BUS;
using JobTestTelerikMvcApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobTestTelerikMvcApp.Controllers
{
    public class SaleController : Controller
    {
        // GET: Sale
        public ActionResult Index()
        {
            string numberBill=WebDB.genNumerBill();
            ViewData["numberBill"] = numberBill;
            return View();
        }

        [HttpPost]
        public ActionResult Edit()
        {
           string NumberBill=Request.Form["VoucherID"].ToString();
           string IODate = Request.Form["IODate"].ToString();
           string RetailID = Request.Form["RetailerID"].ToString();
           string GoodsIDAr=Request.Form["GoodsID[]"];
           return View();
        }
        public JsonResult GetMoneyTypeList()
        {
            return Json(WebDB.getMTList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetUnitList()
        {
            return Json(WebDB.getUnitList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetProductList()
        {
            return Json(WebDB.getProductList(),JsonRequestBehavior.AllowGet);
        }
        public string GetRetailerDetail(long ID)
        {
            return WebDB.getDis(ID);
        }
        public ActionResult getRetailList(string filter)
        {
            if (Request.QueryString.AllKeys.Contains("filter[filters][0][value]"))
            {
                filter = Request.QueryString["filter[filters][0][value]"].ToString();
            }
            //string item = "";

            //List<RetailDTO> list=new List<RetailDTO>();
            //for (int i = 1; i <= 10; i++)
            //{
               // RetailDTO dto = new RetailDTO();
                //dto.ID = "NBL0000"+i;
                //dto.Name = "A"+i;
                //dto.FullAddress = "Vĩnh Ninh | Huế";
             
                //string itemFormat="<li tabindex=\"-1\" role=\"option\" unselectable=\"on\" class=\"k-item\"> <span class=\"k-state-default\">{0}</span><span class=\"k-state-default\"><h3>{1}</h3><p>{2}</p></span></li>";
                //item = string.Format(itemFormat, dto.ID, dto.Name, dto.Address);
                //string text = "<ul unselectable=\"on\" class=\"k-list k-reset\" tabindex=\"-1\" role=\"listbox\" aria-hidden=\"true\" id=\"RetailerID_listbox\" aria-live=\"polite\" style=\"overflow: auto; height: 236.777777671814px;\"><li tabindex=\"-1\" role=\"option\" unselectable=\"on\" class=\"k-item\"> Chọn điểm bán lẻ</li>{0}</ul>";
                
                //list.Add(dto);
            //}
            List<RetailDTO> retailList=WebDB.getRetaiList();
            if (!String.IsNullOrEmpty(filter))
            {
                return Json(retailList.Where(w => w.ID.Equals(int.Parse(filter))|| w.Name.ToLower().Contains(filter.ToLower()) || w.FullAddress.ToLower().Contains(filter.ToLower())).ToList(), JsonRequestBehavior.AllowGet);
            }
            return Json(retailList, JsonRequestBehavior.AllowGet);
        }
    }
}