using JobTestTelerikMvcApp.BUS;
using JobTestTelerikMvcApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobTestTelerikMvcApp.Controllers
{
    public class SaleController : Controller
    {
        // GET: Sale
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Create()
        {
            string numberBill = WebDB.genNumerBill();
            ViewData["BillNumber"] = numberBill;
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(string type)
        {
            try
            {
                using (minhhoa_demoEntities db = WebDB.getDB())
                {
                    string NumberBill = "";
                    if (Request.Form.AllKeys.Contains("VoucherID"))
                    {
                        NumberBill = Request.Form["VoucherID"].ToString();
                    }
                    else
                    {
                        ModelState.AddModelError("Số phiếu", "Chưa có số phiếu");
                    }
                    string Description = "";
                    if (Request.Form.AllKeys.Contains("Description"))
                    {
                        Description = Request.Form["Description"].ToString();
                    }
                    string IODate = "";
                    if (Request.Form.AllKeys.Contains("IODate"))
                    {
                        IODate = Request.Form["IODate"].ToString();
                    }
                    string RetailID = "";
                    if (Request.Form.AllKeys.Contains("RetailerID") && string.IsNullOrEmpty(Request.Form["RetailerID"]) == false)
                    {
                        RetailID = Request.Form["RetailerID"].ToString();
                    }
                    else
                    {
                        ModelState.AddModelError("Điểm bán lẽ", "Chưa chọn điểm bán lẽ");
                    }
                    string CurrencyID = "";
                    if (Request.Form.AllKeys.Contains("CurrencyID") && string.IsNullOrEmpty(Request.Form["CurrencyID"]) == false)
                    {
                        CurrencyID = Request.Form["CurrencyID"].ToString();
                    }
                    else
                    {
                        ModelState.AddModelError("Tiền tệ", "Chưa chọn tiền tệ");
                    }
                    List<string> GoodsIDAr = new List<string>();
                    if (Request.Form.AllKeys.Contains("GoodsID[]") && string.IsNullOrEmpty(Request.Form["GoodsID[]"]) == false)
                    {
                        GoodsIDAr = Request.Form["GoodsID[]"].Split(',').ToList();
                    }
                    List<string> UnitIDAr = new List<string>();
                    if (Request.Form.AllKeys.Contains("UnitID[]") && string.IsNullOrEmpty(Request.Form["UnitID[]"]) == false)
                    {
                        UnitIDAr = Request.Form["UnitID[]"].Split(',').ToList();
                    }
                    List<string> QuantityAr = new List<string>(); ;
                    if (Request.Form.AllKeys.Contains("Quantity[]") && string.IsNullOrEmpty(Request.Form["Quantity[]"]) == false)
                    {
                        QuantityAr = Request.Form["Quantity[]"].Split(',').ToList();
                    }
                    string Discount;
                    if (Request.Form.AllKeys.Contains("Discount") && string.IsNullOrEmpty(Request.Form["Discount"]) == false)
                    {
                        Discount = Request.Form["Discount"];
                    }
                    else
                    {
                        Discount = "0";
                    }
                    string Tax;
                    if (Request.Form.AllKeys.Contains("TaxValue") && string.IsNullOrEmpty(Request.Form["TaxValue"]) == false)
                    {
                        Tax = Request.Form["TaxValue"];
                    }
                    else
                    {
                        Tax = "0";
                    }
                    if (!ModelState.IsValid)
                    {
                        string numberBill = WebDB.genNumerBill();
                        ViewData["BillNumber"] = numberBill;
                        return View("Create");
                    }
                    Bill b = new Bill();
                    b.BillNumber = NumberBill;
                    b.DateTime = DateTime.ParseExact(IODate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    b.CurrencyUnit = CurrencyID;
                    b.RetailID = long.Parse(RetailID);
                    b.Name = "Trần Minh Hòa";
                    b.Discount = long.Parse(Discount);
                    b.Tax = long.Parse(Tax);
                    b.Note = Description;
                    db.Bills.Add(b);
                    db.SaveChanges();
                    for (int i = 0; i < GoodsIDAr.Count; i++)
                    {
                        Bill_Product b_p = new Bill_Product();
                        b_p.BillID = b.ID;
                        b_p.ProductID = long.Parse(GoodsIDAr[i]);
                        b_p.Quantity = long.Parse(QuantityAr[i]);
                        b_p.TypeID = long.Parse(UnitIDAr[i]);
                        b.Bill_Product.Add(b_p);
                    }
                    db.SaveChanges();

                    //var disName = db.Distributors.Where(w => w.ID == b.RetailID).Select(s => s.Name).ToList().ElementAt(0);
                    //ViewBag.Bill = b;
                    //ViewBag.DisName = disName;
                    return RedirectToAction("Edit", new { id = b.BillNumber });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            using (minhhoa_demoEntities db = WebDB.getDB())
            {
                var B = db.Bills.Where(w => w.BillNumber.Equals(id)).ToList();
                if (B.Count == 1)
                {
                    long reID= B[0].RetailID.Value;
                    var Dis = db.Retails.Where(w => w.ID.Equals(reID)).Select(s => s.Distributor.Name).ToList();
                    ViewBag.Bill = B[0];
                    if (Dis.Count() == 1)
                    {
                        ViewBag.DisName = Dis[0].ToString();
                    }
                    return View();
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult Edit()
        {
            try
            {
                using (minhhoa_demoEntities db = WebDB.getDB())
                {
                    string NumberBill = "";
                    if (Request.Form.AllKeys.Contains("VoucherID"))
                    {
                        NumberBill = Request.Form["VoucherID"].ToString();
                    }
                    else
                    {
                        ModelState.AddModelError("Số phiếu", "Chưa có số phiếu");
                    }
                    string Description = "";
                    if (Request.Form.AllKeys.Contains("Description"))
                    {
                        Description = Request.Form["Description"].ToString();
                    }
                    string IODate = "";
                    if (Request.Form.AllKeys.Contains("IODate"))
                    {
                        IODate = Request.Form["IODate"].ToString();
                    }
                    string RetailID = "";
                    if (Request.Form.AllKeys.Contains("RetailerID") && string.IsNullOrEmpty(Request.Form["RetailerID"]) == false)
                    {
                        RetailID = Request.Form["RetailerID"].ToString();
                    }
                    else
                    {
                        ModelState.AddModelError("Điểm bán lẽ", "Chưa chọn điểm bán lẽ");
                    }
                    string CurrencyID = "";
                    if (Request.Form.AllKeys.Contains("CurrencyID") && string.IsNullOrEmpty(Request.Form["CurrencyID"]) == false)
                    {
                        CurrencyID = Request.Form["CurrencyID"].ToString();
                    }
                    else
                    {
                        ModelState.AddModelError("Tiền tệ", "Chưa chọn tiền tệ");
                    }
                    List<string> GoodsIDAr = new List<string>();
                    if (Request.Form.AllKeys.Contains("GoodsID[]") && string.IsNullOrEmpty(Request.Form["GoodsID[]"]) == false)
                    {
                        GoodsIDAr = Request.Form["GoodsID[]"].Split(',').ToList();
                    }
                    List<string> UnitIDAr = new List<string>();
                    if (Request.Form.AllKeys.Contains("UnitID[]") && string.IsNullOrEmpty(Request.Form["UnitID[]"]) == false)
                    {
                        UnitIDAr = Request.Form["UnitID[]"].Split(',').ToList();
                    }
                    List<string> QuantityAr = new List<string>(); ;
                    if (Request.Form.AllKeys.Contains("Quantity[]") && string.IsNullOrEmpty(Request.Form["Quantity[]"]) == false)
                    {
                        QuantityAr = Request.Form["Quantity[]"].Split(',').ToList();
                    }
                    string Discount;
                    if (Request.Form.AllKeys.Contains("Discount") && string.IsNullOrEmpty(Request.Form["Discount"]) == false)
                    {
                        Discount = Request.Form["Discount"];
                    }
                    else
                    {
                        Discount = "0";
                    }
                    string Tax;
                    if (Request.Form.AllKeys.Contains("TaxValue") && string.IsNullOrEmpty(Request.Form["TaxValue"]) == false)
                    {
                        Tax = Request.Form["TaxValue"];
                    }
                    else
                    {
                        Tax = "0";
                    }


                    Bill b = db.Bills.Where(w => w.BillNumber.Equals(NumberBill)).ToList().ElementAt(0);
                    b.BillNumber = NumberBill;
                    b.DateTime = DateTime.ParseExact(IODate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    b.CurrencyUnit = CurrencyID;
                    if (string.IsNullOrEmpty(RetailID) == false)
                    {
                        b.RetailID = long.Parse(RetailID);
                    }
                    else
                    {
                        b.RetailID = -1;
                    }
                    b.Name = "Trần Minh Hòa";
                    b.Discount = long.Parse(Discount);
                    b.Tax = long.Parse(Tax);
                    b.Note = Description;
                    if (ModelState.IsValid == true)
                    {
                        db.SaveChanges();
                        db.Database.ExecuteSqlCommand("Delete From Bill_Product where BillID=" + b.ID);
                    }
                    for (int i = 0; i < GoodsIDAr.Count; i++)
                    {
                        Bill_Product b_p = new Bill_Product();
                        b_p.BillID = b.ID;
                        b_p.ProductID = long.Parse(GoodsIDAr[i]);
                        b_p.Quantity = long.Parse(QuantityAr[i]);
                        b_p.TypeID = long.Parse(UnitIDAr[i]);
                        b.Bill_Product.Add(b_p);
                        // b.Bill_Product=b_p);
                    }
                    if (ModelState.IsValid == true)
                    {
                        int affected = db.SaveChanges();
                    }

                    var disName = db.Distributors.Where(w => w.ID == b.RetailID).Select(s => s.Name).ToList();
                    string disName2 = "";
                    if (disName.Count > 0)
                    {
                        disName2 = disName.ElementAt(0);
                    }
                    ViewBag.Bill = b;
                    ViewBag.DisName = disName2;
                    //ViewData = (ViewDataDictionary)TempData["ViewData"];
                    return View("Edit");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public JsonResult GetMoneyTypeList()
        {
            return Json(WebDB.getMTList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetUnitList()
        {
            return Json(WebDB.getUnitList().OrderBy(o => o.UnitID).ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetProductList()
        {
            return Json(WebDB.getProductList().OrderBy(o => o.GoodsID).ToList(), JsonRequestBehavior.AllowGet);
        }
        public string GetRetailerDetail(long ID)
        {
            return WebDB.getDis(ID);
        }
        public JsonResult getRetailList(string filter)
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
            List<RetailDTO> retailList = WebDB.getRetaiList();
            if (!String.IsNullOrEmpty(filter))
            {
                return Json(retailList.Where(w => w.ID.ToString().Equals(filter) || w.Name.ToLower().Contains(filter.ToLower()) || w.FullAddress.ToLower().Contains(filter.ToLower())).ToList(), JsonRequestBehavior.AllowGet);
            }
            return Json(retailList, JsonRequestBehavior.AllowGet);
        }
    }
}