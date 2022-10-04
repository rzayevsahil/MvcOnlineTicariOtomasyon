using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Services.Description;
using System.Web.UI;
using MvcOnlineTicariOtomasyon.Models.Classes;
using PagedList;
using Message = MvcOnlineTicariOtomasyon.Models.Classes.Message;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class ConcubinePanelController : Controller
    {
        // GET: ConcubinePanel
        private Context context = new Context();
        public ActionResult Index()
        {
            //var mail = (string) Session["ConcubineMail"];
            var mail = Session["ConcubineMail"].ToString();
            var values = context.Messages.Where(x => x.MessageRecipient == mail).ToList();
            ViewBag.mail = mail;
            var mailID = context.Concubines.Where(x => x.ConcubineMail == mail).Select(y => y.ConcubineID).FirstOrDefault();
            
            if (context.SalesMovements?.Where(x => x.ConcubineId == mailID)?.Count() > 0)
            {
                var totalSales = context.SalesMovements.Where(x => x.ConcubineId == mailID)?.Count();
                var totalAmount = context.SalesMovements.Where(x => x.ConcubineId == mailID)?.Sum(x => x.TotalAmount);
                var totalProductCount = context.SalesMovements.Where(x => x.ConcubineId == mailID)?.Sum(x => x.Piece);
                ViewBag.totalSales = totalSales;
                ViewBag.totalAmount = totalAmount;
                ViewBag.totalProductCount = totalProductCount;
            }
            else
            {
                ViewBag.totalSales = 0;
                ViewBag.totalAmount = 0;
                ViewBag.totalProductCount = 0;
            }
            var nameSurname = context.Concubines.Where(x => x.ConcubineMail == mail)
                .Select(x => x.ConcubineName + " " + x.ConcubineSurname).FirstOrDefault();
            ViewBag.nameSurname = nameSurname;
            var city = context.Concubines.Where(x => x.ConcubineMail == mail)
                .Select(x => x.ConcubineCity).FirstOrDefault();
            ViewBag.city = city;
            var lastAnnouncement = context.Messages.Where(x => x.MessageSender == "admin").ToList().Last().MessageContents;
            ViewBag.lastAnnouncement = lastAnnouncement;
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
            var messages = context.Messages.Where(x => x.MessageRecipient == mail).OrderByDescending(x => x.MessageID).ToList();
            var incomingMessages = context.Messages.Count(x => x.MessageRecipient == mail).ToString();
            ViewBag.incomingMessages = incomingMessages;
            var outgoingingMessages = context.Messages.Count(x => x.MessageSender == mail).ToString();
            ViewBag.outgoingingMessages = outgoingingMessages;
            return View(messages);
        }

        [Authorize]
        public ActionResult OutgoingMessages()
        {
            var mail = Session["ConcubineMail"].ToString();
            var messages = context.Messages.Where(x => x.MessageSender == mail).OrderByDescending(x => x.MessageID).ToList();
            var incomingMessages = context.Messages.Count(x => x.MessageRecipient == mail).ToString();
            ViewBag.incomingMessages = incomingMessages;
            var outgoingingMessages = context.Messages.Count(x => x.MessageSender == mail).ToString();
            ViewBag.outgoingingMessages = outgoingingMessages;
            return View(messages);
        }

        public ActionResult MessageDetail(int id)
        {
            var messageDetail = context.Messages.Where(x => x.MessageID == id).ToList();
            var mail = Session["ConcubineMail"].ToString();
            var incomingMessages = context.Messages.Count(x => x.MessageRecipient == mail).ToString();
            ViewBag.incomingMessages = incomingMessages;
            var outgoingingMessages = context.Messages.Count(x => x.MessageSender == mail).ToString();
            ViewBag.outgoingingMessages = outgoingingMessages;
            var message = context.Messages.FirstOrDefault(x => x.MessageID == id);
            var value = "";
            if (message.MessageSender == mail)
            {
                value = "Kime: " + message.MessageRecipient;
            }
            else
            {
                value = "Kimden: " + message.MessageSender;
            }
            ViewBag.value = value;
            return View(messageDetail);
        }

        [HttpGet]
        public ActionResult NewMessage()
        {
            var mail = Session["ConcubineMail"].ToString();
            var incomingMessages = context.Messages.Count(x => x.MessageRecipient == mail).ToString();
            ViewBag.incomingMessages = incomingMessages;
            var outgoingingMessages = context.Messages.Count(x => x.MessageSender == mail).ToString();
            ViewBag.outgoingingMessages = outgoingingMessages;
            return View();
        }
        [HttpPost]
        public ActionResult NewMessage(Message message)
        {
            var mail = Session["ConcubineMail"].ToString();
            message.MessageDate = DateTime.Parse(DateTime.Now.ToString());
            message.MessageSender = mail;
            context.Messages.Add(message);
            context.SaveChanges();
            return RedirectToAction("IncomingMessages");
        }

        public ActionResult CargoTracking(string p, int page = 1)
        {
            var mail = Session["ConcubineMail"].ToString();
            var concubine = context.Concubines.FirstOrDefault(x => x.ConcubineMail == mail);
            var concubineCargoList = context.Concubines.FirstOrDefault(x => (x.ConcubineName.ToUpper() + " " + x.ConcubineSurname.ToUpper()) == (concubine.ConcubineName.ToUpper() + " " + concubine.ConcubineSurname.ToUpper()));
            var values = from x in context.CargoDetails select x;
            var cargoCount = values.Where(x => x.Employee.ToUpper() == concubineCargoList.ConcubineName.ToUpper() + " " + concubineCargoList.ConcubineSurname.ToUpper()).ToList().ToPagedList(page, 5).Count();
            if (cargoCount == 0)
            {
                ViewBag.cargoCount = false;
            }
            else
            {
                ViewBag.cargoCount = true;
            }

            if (!string.IsNullOrEmpty(p))
            {
                values = values.Where(x => x.TrackingCode.Contains(p));
            }

            var cargoes = values.Where(x => x.Employee.ToUpper() == concubineCargoList.ConcubineName.ToUpper() + " " + concubineCargoList.ConcubineSurname.ToUpper()).ToList().ToPagedList(page, 5);
            ViewBag.cgl = concubine.ConcubineName.ToUpper() + " " + concubine.ConcubineSurname.ToUpper() + concubineCargoList;
            return View(cargoes);
        }

        public ActionResult CargoTrackingFindID(string id)
        {
            var values = context.CargoTrackings.Where(x => x.TrackingCode == id).ToList();
            ViewBag.trackingCode = id;
            return View(values);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }

        public PartialViewResult PartialEditProfile()
        {
            var mail = Session["ConcubineMail"].ToString();
            var id = context.Concubines.Where(x => x.ConcubineMail == mail).Select(y => y.ConcubineID).FirstOrDefault();
            var concubine = context.Concubines.Find(id);
            return PartialView("PartialEditProfile", concubine);
        }

        public PartialViewResult PartialAnnouncements()
        {
            var announcements = context.Messages.Where(x => x.MessageSender == "admin").OrderByDescending(x => x.MessageID).ToList();
            return PartialView(announcements);
        }

        public ActionResult ConcubineInfoUpdate(Concubine concubine)
        {
            var _concubine = context.Concubines.Find(concubine.ConcubineID);
            _concubine.ConcubineName = concubine.ConcubineName;
            _concubine.ConcubineSurname = concubine.ConcubineSurname;
            _concubine.ConcubineMail = concubine.ConcubineMail;
            _concubine.ConcubineCity = concubine.ConcubineCity;
            _concubine.ConcubinePassword = concubine.ConcubinePassword;
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}