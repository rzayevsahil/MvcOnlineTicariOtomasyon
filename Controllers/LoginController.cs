using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcOnlineTicariOtomasyon.Models.Classes;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        private Context context = new Context();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public PartialViewResult ConcubineRegister()
        {
            return PartialView();
        }
        [HttpPost]
        public PartialViewResult ConcubineRegister(Concubine concubine)
        {
            context.Concubines.Add(concubine);
            context.SaveChanges();
            return PartialView();
        }

        [HttpGet]
        public ActionResult ConcubineLogIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ConcubineLogIn(Concubine concubine)
        {
            var value = context.Concubines.FirstOrDefault(x =>
                x.ConcubineMail == concubine.ConcubineMail && x.ConcubinePassword == concubine.ConcubinePassword);
            if (value != null)
            {
                FormsAuthentication.SetAuthCookie(value.ConcubineMail,false);
                //Session["ConcubineMail"] = value.ConcubineMail.ToString();
                Session.Add("ConcubineMail", value.ConcubineMail.ToString());
                return RedirectToAction("Index", "ConcubinePanel");
            }
            return RedirectToAction("Index","Login");
        }

        [HttpGet]
        public ActionResult AdminLogIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdminLogIn(Admin admin)
        {
            var values =
                context.Admins.FirstOrDefault(x => x.UserName == admin.UserName && x.Password == admin.Password);
            if (values != null)
            {
                FormsAuthentication.SetAuthCookie(values.UserName, false);
                Session["UserName"] = values.UserName.ToString();
                return RedirectToAction("Index", "Category");
            }
            return RedirectToAction("Index","Login");
        }
    }
}