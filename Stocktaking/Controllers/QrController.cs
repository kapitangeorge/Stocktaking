using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QRCoder;
using Stocktaking.Data;
using Stocktaking.Data.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Stocktaking.Controllers
{
    public class QrController : Controller
    {
        private ApplicationContext database;

        public QrController(ApplicationContext context)
        {
            database = context;
        }

        [HttpGet]
        public async Task<IActionResult> QrCode(string inventoryNumber)
        {
            Item item = await database.Items.FirstOrDefaultAsync(r => r.InventoryNumber == inventoryNumber);
            return View(item);
        }
        [HttpGet]
        public IActionResult GetQRCode(string inventoryNumber)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(inventoryNumber, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);

            FileContentResult result;

            using (var memStream = new System.IO.MemoryStream())
            {
                qrCodeImage.Save(memStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                result = this.File(memStream.GetBuffer(), "image/jpeg");
            }

            return result;
        }

        
    }
}
