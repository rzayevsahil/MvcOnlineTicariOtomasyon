using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Classes;
using WebGrease.Css.Ast.Selectors;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class StatisticsController : Controller
    {
        // GET: Statistics
        private Context context = new Context();
        public ActionResult Index()
        {
            var value1 = context.Concubines.Count().ToString();
            ViewBag.d1 = value1;

            var value2 = context.Products.Count().ToString();
            ViewBag.d2 = value2;

            var value3 = context.Employees.Where(x => x.Situation == true).Count().ToString();
            ViewBag.d3 = value3;

            var value4 = context.Categories.Count().ToString();
            ViewBag.d4 = value4;

            var value5 = context.Products.Sum(x => x.Stock).ToString();
            ViewBag.d5 = value5;

            var value6 = (from product in context.Products where (product.Situation == true) select product.Brand).Distinct().Count().ToString();
            ViewBag.d6 = value6;

            var value7 = context.Products.Count(x => x.Stock <= 20).ToString();
            ViewBag.d7 = value7;

            var value8 =
                (from product in context.Products orderby product.SalesPrice descending select product.ProductName)
                .FirstOrDefault();
            ViewBag.d8 = value8;

            var value9 = (from product in context.Products
                          where (product.Situation == true)
                          orderby product.SalesPrice ascending
                          select product.ProductName).FirstOrDefault();
            ViewBag.d9 = value9;

            var value10 = context.Products.Count(x => x.ProductName == "Buzdolabı").ToString();
            ViewBag.d10 = value10;

            var value11 = context.Products.Count(x => x.ProductName == "Laptop").ToString();
            ViewBag.d11 = value11;

            var value12 = context.Products.GroupBy(x => x.Brand).OrderByDescending(z => z.Count()).Select(y => y.Key).FirstOrDefault();
            ViewBag.d12 = value12;

            var value13 =context.Products.Where(p=>p.ProductID == (context.SalesMovements.GroupBy(x => x.ProductId).OrderByDescending(z => z.Count())
                .Select(y => y.Key).FirstOrDefault())).Select(k=>k.ProductName).FirstOrDefault();
            ViewBag.d13 = value13;

            var value14 = context.SalesMovements.Sum(x => x.TotalAmount).ToString();
            ViewBag.d14 = value14;

            DateTime today = DateTime.Today;
            var value15 = context.SalesMovements.Count(x => x.Date == today).ToString();
            ViewBag.d15 = value15;

            var value16 = context.SalesMovements.Where(x => x.Date == today).Sum(x => (decimal?)x.TotalAmount).ToString();
            if (string.IsNullOrEmpty(value16))
            {
                value16 = "0";
            }
            ViewBag.d16 = value16;
            return View();
        }

        public ActionResult ProgressTables()
        {
            var query = from x in context.Concubines where x.Situation==true
                group x by x.ConcubineCity
                into g
                select new ClassGroup
                {
                    City = g.Key,
                    Count = g.Count()
                };
            var totalCount = context.Concubines.Count().ToString();
            ViewBag.val1 = totalCount;
            return View(query.ToList());
        }

        public PartialViewResult Partial()
        {
            var query = context.Categories.ToList();
            return PartialView(query);
        }

        public PartialViewResult Partial1()
        {
            var query = from x in context.Employees where x.Situation==true
                group x by x.Department.DepartmentName
                into g
                select new ClassGroup2
                {
                    DepartmentName = g.Key,
                    Count = g.Count()
                };
            return PartialView(query.ToList());
        }

        public PartialViewResult Partial2()
        {
            var query = context.Concubines.ToList();
            return PartialView(query);
        }

        public PartialViewResult Partial3()
        {
            var query = context.Products.ToList();
            return PartialView(query);
        }

        public PartialViewResult Partial4()
        {
            var query = from x in context.Products
                where x.Situation == true
                group x by x.Brand
                into g
                select new ClassGroup3
                {
                    Brand = g.Key,
                    Count = g.Count()
                };
            return PartialView(query.ToList());
        }
    }
}