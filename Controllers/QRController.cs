using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QRCoder;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    [Authorize]
    public class QRController : Controller
    {
        // GET: QR
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string code)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                QRCodeGenerator qrCodeGenerator = new QRCodeGenerator();
                QRCodeGenerator.QRCode qrCode = qrCodeGenerator.CreateQrCode(code,QRCodeGenerator.ECCLevel.Q);
                using (Bitmap image = qrCode.GetGraphic(10))
                {
                    image.Save(memoryStream, ImageFormat.Png);
                    ViewBag.qrCodeImage = "data:image/png;base64," + Convert.ToBase64String(memoryStream.ToArray());
                }
            }
            return View();
        }
    }
}