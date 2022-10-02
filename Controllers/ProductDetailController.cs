using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Classes;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    [Authorize]
    public class ProductDetailController : Controller
    {
        // GET: ProductDetail
        Context context = new Context();
        public ActionResult Index()
        {
            //var values = context.Products.Where(x => x.Situation == true).Where(y=>y.ProductID==1).ToList();
            Class1 class1 = new Class1();
            class1.Value1 = context.Products.Where(x => x.Situation == true).Where(y => y.ProductID == 1).ToList();
            class1.Value2 = context.Details.Where(y => y.DetailID == 1).ToList();
            return View(class1);
        }
    }
}