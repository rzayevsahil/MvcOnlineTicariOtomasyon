using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Classes;
namespace MvcOnlineTicariOtomasyon.Controllers
{
    [Authorize]
    public class GaleryController : Controller
    {
        // GET: Galery
        Context context = new Context();
        public ActionResult Index()
        {
            var values = context.Products.Where(x=>x.Situation==true).ToList();
            return View(values);
        }
    }
}