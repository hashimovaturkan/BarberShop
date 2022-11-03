using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using QRCoder;
using ZXing.QrCode;
using ZXing;

namespace BarberShop.Application.Common.Extensions
{
    public static partial class Extension
    {
        public static string QrCodeGenerate(this string userDetails, IWebHostEnvironment _environment)
        {
            var writer = new QRCodeWriter();
            var resultBit = writer.encode(userDetails, BarcodeFormat.QR_CODE, 200, 200);
            var matrix = resultBit;
            int scale = 1;
            Bitmap result = new Bitmap(matrix.Width * scale, matrix.Height * scale);

            for (int x = 0; x < matrix.Height; x++)
            {
                for (int y = 0; y < matrix.Width; y++)
                {
                    Color pixel = matrix[x,y] ? Color.Black : Color.White;

                    for (int i = 0; i < scale; i++)
                    {
                        for (int j = 0; j < scale; j++)
                        {
                            result.SetPixel(x * scale + i, y * scale + j, pixel);
                        }
                    }
                }
            }

            string webRootPath = _environment.WebRootPath;
            result.Save(webRootPath + "\\qrImage\\qrcode.png");

            return webRootPath + "\\qrImage\\qrcode.png";



            //GeneratedBarcode barcode = QRCodeWriter.CreateQrCode(userDetails, 200);

            //barcode.SetMargins(10);
            //barcode.ChangeBarCodeColor(Color.Black);

            //var imageUrl = $"qrcode.png";

            //string physicalFileName = Path.Combine(_environment.ContentRootPath,
            //                                       "wwwroot",
            //                                       "qrImage",
            //                                       imageUrl);

            //barcode.SaveAsPng(physicalFileName);

            //return physicalFileName;
        }
    }
}
