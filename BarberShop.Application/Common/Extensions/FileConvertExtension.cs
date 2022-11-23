using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
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

            using (var stream = new MemoryStream(bytes))
            {
                stream.Position = 0;
                var formFile = new FormFile(stream, 0, stream.Length, link, link);
                if (stream != null) stream.Dispose();
                return formFile;
            }

        }
    }
}
