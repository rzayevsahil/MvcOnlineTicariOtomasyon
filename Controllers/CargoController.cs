using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Classes;
using Newtonsoft.Json.Linq;
using PagedList;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    [Authorize]
    public class CargoController : Controller
    {
        // GET: Cargo
        Context context = new Context();

        public ActionResult Index(string p, int page = 1)
        {
            var values = from x in context.CargoDetails select x;
            if (!string.IsNullOrEmpty(p))
            {
                values = values.Where(x => x.TrackingCode.Contains(p));
            }

            var cargoes = values.ToList().ToPagedList(page, 5);
            return View(cargoes);
        }

        [HttpGet]
        public ActionResult CargoAdd()
        {
            Random random = new Random();
            string[] codeChar = { "A", "B", "C", "D", "E", "F", "G", "H" };
            int ch1, ch2, ch3;
            ch1 = random.Next(0, codeChar.Length);
            ch2 = random.Next(0, codeChar.Length);
            ch3 = random.Next(0, codeChar.Length);
            int num1, num2, num3;
            num1 = random.Next(100, 1000);
            num2 = random.Next(10, 99);
            num3 = random.Next(10, 99);
            string trackingCode = num1 + codeChar[ch1] + num2 + codeChar[ch2] + num3 + codeChar[ch3];
            ViewBag.trackingCode = trackingCode;
            return View();
        }

        [HttpPost]
        public ActionResult CargoAdd(CargoDetail cargoDetail)
        {
            context.CargoDetails.Add(cargoDetail);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CargoTracking(string id)
        {
            var values = context.CargoTrackings.Where(x => x.TrackingCode == id).ToList();
            ViewBag.trackingCode = id;
            return View(values);
        }

        public ActionResult CargoTrackingAdd(string id)
        {
            ViewBag.trackingCode = id;
            return View();
        }
        [HttpPost]
        public ActionResult CargoTrackingAdd(CargoTracking cargoTracking)
        {
            context.CargoTrackings.Add(cargoTracking);
            ViewBag.trackingCode = cargoTracking.TrackingCode;
            context.SaveChanges();
            return RedirectToAction("CargoTracking/" + ViewBag.trackingCode);
        }

        public ActionResult CargoTrackingDelete(int id)
        {
            var value = context.CargoTrackings.Find(id);
            ViewBag.trackingCode = value.TrackingCode;
            context.CargoTrackings.Remove(value);
            context.SaveChanges();
            return RedirectToAction("CargoTracking/" + ViewBag.trackingCode);
        }

        public ActionResult CargoTrackingUpdate(int id)
        {
            var value = context.CargoTrackings.Find(id);
            ViewBag.trackingCode = value.TrackingCode;
            var date = value.Datetime.ToString("yyyy-MM-dd");
            var time = value.Datetime.ToString("HH:mm");
            ViewBag.datetime=date+"T"+time;
            return View(value);
        }
        [HttpPost]
        public ActionResult CargoTrackingUpdate(CargoTracking ct)
        {
            var cargoTracking = context.CargoTrackings.Find(ct.CargoTrackingID);
            cargoTracking.CargoTrackingID = ct.CargoTrackingID;
            cargoTracking.TrackingCode = ct.TrackingCode;
            cargoTracking.Datetime = ct.Datetime;
            cargoTracking.Explanation = ct.Explanation;
            ViewBag.trackingCode = ct.TrackingCode;
            context.SaveChanges();
            return RedirectToAction("CargoTracking/" + ViewBag.trackingCode);
        }
    }
}