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
            return View();
        }

        public ActionResult getRetailList(string filter)
        {
            if (Request.QueryString.AllKeys.Contains("filter[filters][0][value]"))
            {
                filter = Request.QueryString["filter[filters][0][value]"].ToString();
            }
            //string item = "";

            List<RetailDTO> list=new List<RetailDTO>();
            for (int i = 1; i <= 10; i++)
            {
                RetailDTO dto = new RetailDTO();
                dto.ID = "NBL0000"+i;
                dto.Name = "A"+i;
                dto.FullAddress = "Vĩnh Ninh | Huế";
             
                //string itemFormat="<li tabindex=\"-1\" role=\"option\" unselectable=\"on\" class=\"k-item\"> <span class=\"k-state-default\">{0}</span><span class=\"k-state-default\"><h3>{1}</h3><p>{2}</p></span></li>";
                //item = string.Format(itemFormat, dto.ID, dto.Name, dto.Address);
                //string text = "<ul unselectable=\"on\" class=\"k-list k-reset\" tabindex=\"-1\" role=\"listbox\" aria-hidden=\"true\" id=\"RetailerID_listbox\" aria-live=\"polite\" style=\"overflow: auto; height: 236.777777671814px;\"><li tabindex=\"-1\" role=\"option\" unselectable=\"on\" class=\"k-item\"> Chọn điểm bán lẻ</li>{0}</ul>";
                
                list.Add(dto);
            }
            if (!String.IsNullOrEmpty(filter))
            {
                return Json(list.Where(w => w.ID.ToLower().Contains(filter.ToLower()) || w.Name.ToLower().Contains(filter.ToLower()) || w.FullAddress.ToLower().Contains(filter.ToLower())).ToList(), JsonRequestBehavior.AllowGet);
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}