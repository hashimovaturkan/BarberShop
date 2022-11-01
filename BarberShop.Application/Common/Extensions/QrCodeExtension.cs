using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronBarCode;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Hosting;

namespace BarberShop.Application.Common.Extensions
{
    public static partial class Extension
    {
        public static string QrCodeGenerate(this string number, IWebHostEnvironment _environment)
        {
            GeneratedBarcode barcode = QRCodeWriter.CreateQrCode(number, 200);
            barcode.AddBarcodeValueTextBelowBarcode();

            barcode.SetMargins(10);
            barcode.ChangeBarCodeColor(Color.Black);

            var imageUrl = $"qrcode.png";

            string physicalFileName = Path.Combine(_environment.ContentRootPath,
                                                   "wwwroot",
                                                   "qrImage",
                                                   imageUrl);

            barcode.SaveAsPng(physicalFileName);

            return physicalFileName;
        }
    }
}
