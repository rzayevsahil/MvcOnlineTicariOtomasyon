using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Classes;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class ConcubinePanelController : Controller
    {
        // GET: ConcubinePanel
        private Context context = new Context();
        [Authorize]
        public ActionResult Index()
        {
            //var mail = (string) Session["ConcubineMail"];
            var mail = Session["ConcubineMail"].ToString();
            var values = context.Concubines.FirstOrDefault(x => x.ConcubineMail == mail);
            ViewBag.mail = mail;
            return View(values);
        }

        public ActionResult MyOrders()
        {
            var mail = Session["ConcubineMail"].ToString();
            var id = context.Concubines.Where(x => x.ConcubineMail == mail.ToString()).Select((y => y.ConcubineID))
                .FirstOrDefault();
            var values = context.SalesMovements.Where(x => x.ConcubineId == id).ToList();
            return View(values);
        }

        public ActionResult IncomingMessages()
        {
            var mail = Session["ConcubineMail"].ToString();
            var messages = context.Messages.Where(x => x.MessageRecipient == mail).ToList();
            var incomingMessages = context.Messages.Count(x => x.MessageRecipient == mail).ToString();
            ViewBag.incomingMessages = incomingMessages;
            var outgoingingMessages = context.Messages.Count(x => x.MessageSender == mail).ToString();
            ViewBag.outgoingingMessages = outgoingingMessages;
            return View(messages);
        }
        public ActionResult OutgoingMessages()
        {
            var mail = Session["ConcubineMail"].ToString();
            var messages = context.Messages.Where(x => x.MessageSender == mail).ToList();
            var incomingMessages = context.Messages.Count(x => x.MessageRecipient == mail).ToString();
            ViewBag.incomingMessages = incomingMessages;
            var outgoingingMessages = context.Messages.Count(x => x.MessageSender == mail).ToString();
            ViewBag.outgoingingMessages = outgoingingMessages;
            return View(messages);
        }
        //[HttpGet]
        //public ActionResult NewMessage()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult NewMessage()
        //{
        //    return View();
        //}
    }
}