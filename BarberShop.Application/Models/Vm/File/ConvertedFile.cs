using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.Models.Vm.File
{
    public class ConvertedFile
    {
        public IFormFile File { get; set; }
        public string Path { get; set; }
    }
}
