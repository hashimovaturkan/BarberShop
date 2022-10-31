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
        private static IWebHostEnvironment _hostingEnvironment;

        public static void Initialize(IWebHostEnvironment hostEnvironment)
        {
            _hostingEnvironment = hostEnvironment;
        }

        public static async Task<string> FileUpload(this IFormFile file)
        {
            string extension = Path.GetExtension(file.FileName);
            var imageUrl = $"{Guid.NewGuid()}{extension}";

            string physicalFileName = Path.Combine(_hostingEnvironment.ContentRootPath,
                                                   "wwwroot",
                                                   "uploads",
                                                   imageUrl);

            using (var stream = new FileStream(physicalFileName, FileMode.Create, FileAccess.Write))
            {
                await file.CopyToAsync(stream);
            }

            return imageUrl;
        }

        public static string GetFile(this string? imageUrl)
        {
            if (imageUrl == null)
                return null;

            string physicalFileName = Path.Combine(_hostingEnvironment.ContentRootPath,
                                                   "wwwroot",
                                                   "uploads",
                                                   imageUrl);

            return physicalFileName;
        }
    }
}
