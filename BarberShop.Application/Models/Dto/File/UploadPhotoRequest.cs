using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.Models.Dto.File
{
    public class UploadPhotoRequest
    {
        public IFormFile Photo { get; set; }
    }
}
