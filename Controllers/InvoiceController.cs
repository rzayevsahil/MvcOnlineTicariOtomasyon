using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Classes;
namespace MvcOnlineTicariOtomasyon.Controllers
{
    [Authorize]
    public class InvoiceController : Controller
    {
        // GET: Invoice
        private Context context = new Context();
        public ActionResult Index()
        {
            var values = context.Invoices.ToList();
            return View(values);
        }
        [HttpGet]
        public ActionResult InvoiceAdd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult InvoiceAdd(Invoice invoice)
        {
            invoice.Date=DateTime.Now;
            invoice.Hour=DateTime.Now.ToString("HH:mm");
            context.Invoices.Add(invoice);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult InvoiceGet(int id)
        {
            var invoice = context.Invoices.Find(id);
            return View("InvoiceGet", invoice);
        }

        public ActionResult InvioceUpdate(Invoice i)
        {
            var invoice = context.Invoices.Find(i.InvoiceID);
            invoice.InvoiceSerialNumber = i.InvoiceSerialNumber;
            invoice.InvoiceRowNumber = i.InvoiceRowNumber;
            invoice.Hour = i.Hour;
            invoice.Date = i.Date;
            invoice.Submitter = i.Submitter;
            invoice.Receiver = i.Receiver;
            invoice.TaxAdministration = i.TaxAdministration;
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult InvoiceDetail(int id)
        {
            var values = context.InvoicePens.Where(x => x.InvoiceId == id).ToList();
            ViewBag.invoiceId = id;
            return View(values);
        }
        [HttpGet]
        public ActionResult InvoicePenAdd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult InvoicePenAdd(int id,InvoicePen ip)
        {
            ip.Amount = ip.UnitPrice * ip.Quantity;
            ip.InvoiceId = id;
            context.InvoicePens.Add(ip);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}