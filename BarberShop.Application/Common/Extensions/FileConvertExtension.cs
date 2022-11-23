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

            using (var stream = new MemoryStream(bytes))
            {
                return new FormFile(stream, 0, stream.Length, "a", "a");
            }

        }
    }
}
