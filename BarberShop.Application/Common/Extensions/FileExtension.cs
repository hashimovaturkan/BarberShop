using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.Common.Extensions
{
    public static partial class Extension
    {

        public static async Task<string> FileUpload(this IFormFile file, IWebHostEnvironment _env)
        {
            string extension = Path.GetExtension(file.FileName);
            var imageUrl = $"{Guid.NewGuid()}{extension}";

            string physicalFileName = Path.Combine(_env.ContentRootPath,
                                                   "wwwroot",
                                                   "uploads",
                                                   imageUrl);

            using (var stream = new FileStream(physicalFileName, FileMode.Create, FileAccess.Write))
            {
                await file.CopyToAsync(stream);
            }

            return imageUrl;
        }

        public static string GetFile(this string? imageUrl, IWebHostEnvironment _env)
        {
            if (imageUrl == null)
                return null;

            string physicalFileName = Path.Combine(_env.ContentRootPath,
                                                   "wwwroot",
                                                   "uploads",
                                                   imageUrl);

            return physicalFileName;
        }
    }
}
