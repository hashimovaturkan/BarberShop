using BarberShop.Persistence.Migrations;
using Microsoft.AspNetCore.Http;
using RestSharp;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BarberShop.Application.Common.Extensions
{
    public static partial class Extension
    {
        public static IFormFile ConvertFile(this string file)
        {
            Byte[] bytes = Convert.FromBase64String(file);
            Random random = new Random();
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            var link  = new string(Enumerable.Repeat(chars, 13)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            FormFile formFile;
            using (var stream = new MemoryStream(bytes))
            {
                stream.Position = 0;
                formFile = new FormFile(stream, 0, stream.Length, link, link)
                {
                    Headers = new HeaderDictionary(),
                    //ContentType = "multipart/form-data",
                };

                System.Net.Mime.ContentDisposition cd = new System.Net.Mime.ContentDisposition
                {
                    FileName = formFile.FileName
                };
                formFile.ContentDisposition = cd.ToString();

                //if (stream != null) stream.Dispose();
                
            }

            return formFile;


        }
    }
}
