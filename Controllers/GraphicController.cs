using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Classes;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class GraphicController : Controller
    {
        // GET: Graphic
        Context context = new Context();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Index2()
        {
            var graphDraw = new Chart(600, 600);
            graphDraw.AddTitle("Kategori - Ürün Stok Sayısı").AddLegend("Stok").AddSeries("Değerler", xValue: new[] { "Mobilya", "Ofis Eşyaları" }, yValues: new[] { 5, 9 }).Write();

            return File(graphDraw.ToWebImage().GetBytes(), "image/jpeg");
        }

        public ActionResult Index3()
        {
            ArrayList xValue = new ArrayList();
            ArrayList yValue = new ArrayList();
            var results = context.Products.ToList();
            results.ToList().ForEach(x => xValue.Add(x.ProductName));
            results.ToList().ForEach(y => yValue.Add(y.Stock));
            var graphic = new Chart(width: 800, height: 800).AddTitle("Stoklar").AddSeries(chartType: "Pie", name: "Stok", xValue: xValue, yValues: yValue);

            return File(graphic.ToWebImage().GetBytes(), "image/jpeg");
        }

        

        //public ActionResult VisualizeProductResult1()
        //{
        //    return Json(ProductList(), JsonRequestBehavior.AllowGet);
        //}

        //public List<ProductClass> ProductList1()
        //{
        //    List<ProductClass> products = new List<ProductClass>();
        //    products.Add(new ProductClass()
        //    {
        //        productName = "Bilgisayar",
        //        stock = 120
        //    });
        //    products.Add(new ProductClass()
        //    {
        //        productName = "TV",
        //        stock = 80
        //    });
        //    products.Add(new ProductClass()
        //    {
        //        productName = "Elektronik",
        //        stock = 98
        //    });
        //    products.Add(new ProductClass()
        //    {
        //        productName = "Araba",
        //        stock = 13
        //    });
        //    return products;
        //}

        public ActionResult GoogleChart1()
        {
            return View();
        }

        public ActionResult ProductGoogleChart()
        {
            return View();
        }

        public ActionResult ProductLineGoogleChart()
        {
            return View();
        }

        public ActionResult VisualizeProductResult()
        {
            return Json(ProductList(), JsonRequestBehavior.AllowGet);
        }

        public List<ProductClass> ProductList()
        {
            List<ProductClass> products = new List<ProductClass>();
            using (var context = new Context())
            {
                products = context.Products.Where(x=>x.Situation==true).Select(x => new ProductClass
                {
                    productName = x.ProductName,
                    stock = x.Stock
                }).ToList();
            }
            return products;
        }

        
    }
}