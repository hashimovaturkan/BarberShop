using BarberShop.Application.Models.Vm.File;
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
using static BarberShop.Application.Common.Components.FileHelperExtension;

namespace BarberShop.Application.Common.Extensions
{
    public static partial class Extension
    {
        private const string _tempFolder = "uploads";
        private static readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(), _tempFolder);
        public static ConvertedFile ConvertFile(this string base64)
        {
            byte[] bytes = Convert.FromBase64String(base64);

            var url = bytes.SaveFileToFolderAndGetPath();

            var photofullPath = Path.Combine(_filePath, url);

            IFormFile fromFile;
            using (var ms = new MemoryStream(bytes))
            {
                fromFile = new FormFile(ms, 0, ms.Length,
                    Path.GetFileNameWithoutExtension(photofullPath),
                    Path.GetFileName(photofullPath)
                );
                
            }

            return new ConvertedFile
            {
                File = fromFile,
                Path = photofullPath
            };


        }
    }
}
