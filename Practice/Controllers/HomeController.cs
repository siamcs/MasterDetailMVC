using Practice.Models;
using Practice.VM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Practice.Controllers
{
    public class HomeController : Controller
    {
        AppDbContext db = new AppDbContext();
        [HttpGet]
        public ActionResult Index(int? id)
        {
            var item = new List<SelectListItem>
            {
                new SelectListItem { Value = "Food", Text = "Food"},
                new SelectListItem { Value = "Machine", Text = "Machine" }
            };
            VmSale osale = null;
            var osm = db.SaleMasters.Where(x => x.SaleId == id).FirstOrDefault();
            if (osm != null)
            {
                osale = new VmSale();
                osale.SaleId = osm.SaleId;
                osale.CustomeName = osm.CustomeName;
                osale.Gender = osm.Gender;
                osale.Photo = osm.Photo;
                osale.Type = osm.Type;
                osale.CreateDate = osm.CreateDate;
                var list = new List<VmSale.VmSaleDetail>();
                var listSD = db.SaleDetails.Where(x => x.SaleId == id).ToList();
                foreach (var oSD in listSD)
                {
                    var osaledetail = new VmSale.VmSaleDetail();
                    osaledetail.SaleDetailId = oSD.SaleDetailId;
                    osaledetail.SaleId = oSD.SaleId;
                    osaledetail.ProductName = oSD.ProductName;
                    osaledetail.Price = oSD.Price;
                    list.Add(osaledetail);
                }
                osale.SaleDetails = list;
            }
            osale = osale == null ? new VmSale() : osale;
            ViewData["List"] = db.SaleMasters.ToList();
            ViewData["item"] = item;
            return View(osale);
        }
        [HttpPost]
        public ActionResult Index(VmSale model,HttpPostedFileBase img,FormCollection form)
        {
            var osalemaster = db.SaleMasters.Find(model.SaleId);
            string F = img?.FileName;
            if(img!=null)
            {
                string P = Path.Combine(Server.MapPath("~/Picture"), F);
                img.SaveAs(P);
            }
            if(osalemaster==null)
            {
                osalemaster = new SaleMaster();
                osalemaster.CustomeName = model.CustomeName;
                osalemaster.Gender = model.Gender;
                osalemaster.Photo = "/Picture/"+F;
                osalemaster.Type = form["select"];
                osalemaster.CreateDate = model.CreateDate;
                db.SaleMasters.Add(osalemaster);    
                var list=new List<SaleDetail>();
                for(var i=0;i<model.ProductName.Length;i++) 
                {
                    if (!string.IsNullOrEmpty(model.ProductName[i]))
                    {
                        var osaledetail=new SaleDetail();   
                        osaledetail.ProductName= model.ProductName[i];
                        osaledetail.Price = model.Price[i];
                        osaledetail.SaleId = osalemaster.SaleId;
                        list.Add(osaledetail);
                    }
                
                }
                db.SaleMasters.Add(osalemaster);
                db.SaleDetails.AddRange(list);
                db.SaveChanges();
            }
            else
            {
                osalemaster.CustomeName = model.CustomeName;
                osalemaster.Gender = model.Gender;
                osalemaster.Photo =(F!=null)? "/Picture/" + F : osalemaster.Photo;
                osalemaster.Type = form["select"];
                osalemaster.CreateDate = model.CreateDate;
                var osalemasterRem = db.SaleMasters.Find(model.SaleId);
                var listSDRem = db.SaleDetails.Where(x => x.SaleId == model.SaleId).ToList();
                db.SaleMasters.Remove(osalemasterRem);
                db.SaleDetails.RemoveRange(listSDRem);
                db.SaveChanges();
                var list = new List<SaleDetail>();
                for (var i = 0; i < model.ProductName.Length; i++)
                {
                    if (!string.IsNullOrEmpty(model.ProductName[i]))
                    {
                        var osaledetail = new SaleDetail();
                        osaledetail.ProductName = model.ProductName[i];
                        osaledetail.Price = model.Price[i];
                        osaledetail.SaleId = osalemaster.SaleId;
                        list.Add(osaledetail);
                    }

                }
                db.SaleMasters.Add(osalemaster);
                db.SaleDetails.AddRange(list);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            var sm = (from o in db.SaleMasters where o.SaleId == id select o).FirstOrDefault();
            var sd = (from o in db.SaleDetails where o.SaleId == id select o).ToList();
            if(sd!=null)
            db.SaleMasters.Remove(sm);
            db.SaleDetails.RemoveRange(sd);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}