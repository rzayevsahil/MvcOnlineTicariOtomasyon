using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Classes;
using Newtonsoft.Json.Linq;
using PagedList;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        Context context = new Context();
        public ActionResult Index(string p, int page = 1)
        {
            var products = from x in context.Products where x.Situation==true select x;
            if (!string.IsNullOrEmpty(p))
            {
                products = products.Where(y => y.ProductName.Contains(p));
            }
            var values = products.ToList().ToPagedList(page, 5);
            return View(values);
        }
        [HttpGet]
        public ActionResult ProductAdd()
        {
            List<SelectListItem> value = (from catogory in context.Categories.ToList() select new SelectListItem
            {
                Text = catogory.CategoryName,
                Value = catogory.CategoryID.ToString()
            }).ToList();
            ViewBag.val = value;
            return View();
        }
        [HttpPost]
        public ActionResult ProductAdd(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ProductDelete(int id)
        {
            var value=context.Products.Find(id);
            value.Situation = false;
            //context.Products.Remove(value);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ProductGet(int id)
        {
            List<SelectListItem> value = (from catogory in context.Categories.ToList()
                select new SelectListItem
                {
                    Text = catogory.CategoryName,
                    Value = catogory.CategoryID.ToString()
                }).ToList();
            ViewBag.val = value;
            var product = context.Products.Find(id);
            return View("ProductGet", product);
        }

        public ActionResult ProductUpdate(Product product)
        {
            var p = context.Products.Find(product.ProductID);
            p.PurchhasePrice=product.PurchhasePrice;
            p.Situation=product.Situation;
            p.CategoryId = product.CategoryId;
            p.Brand=product.Brand;
            p.SalesPrice=product.SalesPrice;
            p.Stock=product.Stock;
            p.ProductName=product.ProductName;
            p.ProductImage=product.ProductImage;
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ProductList()
        {
            var values = context.Products.Where(p => p.Situation == true).ToList();
            return View(values);
        }

        public ActionResult ProductDetail(int id)
        {
            var value = context.Products.Where(x => x.Situation == true).Where(y => y.ProductID == id).ToList();

            ViewBag.val1 = context.Details.Where(y => y.DetailID == id)?.First().ProductName.ToString();
            ViewBag.val2 = context.Details.Where(y => y.DetailID == id)?.FirstOrDefault().ProductInformation.ToString();
            return View(value);
        }
        [HttpGet]
        public ActionResult ProductMakeSale(int id)
        {
            List<SelectListItem> value1 = (from x in context.Employees.Where(e => e.Situation == true).ToList()
                select new SelectListItem()
                {
                    Text = x.EmployeeName + " " + x.EmployeeSurname,
                    Value = x.EmployeeID.ToString()
                }).ToList();
            ViewBag.value1 = value1;
            var value2 = context.Products.Find(id);
            ViewBag.value2 = value2.ProductID;
            ViewBag.value3 = value2.SalesPrice;
            return View();
        }
        [HttpPost]
        public ActionResult ProductMakeSale(SalesMovement salesMovement)
        {
            salesMovement.Date = DateTime.Parse(DateTime.Now.ToShortDateString());
            salesMovement.Situation = true;
            context.SalesMovements.Add(salesMovement);
            context.SaveChanges();
            return RedirectToAction("Index","Sales");
        }
    }
}