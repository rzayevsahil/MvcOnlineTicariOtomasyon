using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Classes;
using PagedList;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        Context c = new Context();
        public ActionResult Index(string p, int page = 1)
        {
            //var - bütün değerlerin türünü alır
            var values = from x in c.Categories select x;
            if (!string.IsNullOrEmpty(p))
            {

                values = values.Where(x => x.CategoryName.Contains(p));
            }

            var categories = values.ToList().ToPagedList(page, 5);
            return View(categories);
        }

        //sayfa ilk yüklendiğinde bu çalışacak boş bişey olduğu için ekleme yapmayacak
        [HttpGet]
        public ActionResult CategoryAdd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CategoryAdd(Category category)
        {
            //context deki category'nin içine ekle
            c.Categories.Add(category);
            c.SaveChanges();//değişiklikleri kayd et
            return RedirectToAction("Index");//işlem bittikten sonra beni Index isimli ActionResult'a yönlendir
        }

        public ActionResult CategoryDelete(int id)
        {
            var category = c.Categories.Find(id);
            c.Categories.Remove(category);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CategoryGet(int id)
        {
            var category = c.Categories.Find(id);
            return View("CategoryGet", category);
        }

        public ActionResult CategoryUpdate(Category ctg)
        {
            var category = c.Categories.Find(ctg.CategoryID);
            category.CategoryName = ctg.CategoryName;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Attempt()
        {
            Class2 class2 = new Class2();
            class2.Categories = new SelectList(c.Categories, "CategoryID", "CategoryName");
            class2.Products = new SelectList(c.Products, "ProductID", "ProductName");
            return View(class2);
        }

        public JsonResult ProductGet(int p)
        {
            var productList = (from product in c.Products
                               join category in c.Categories on product.Category.CategoryID equals category.CategoryID
                               where product.Category.CategoryID == p
                               select new
                               {
                                   Text = product.ProductName,
                                   Value = category.CategoryID.ToString()
                               }).ToList();
            return Json(productList, JsonRequestBehavior.AllowGet);
        }
    }
}