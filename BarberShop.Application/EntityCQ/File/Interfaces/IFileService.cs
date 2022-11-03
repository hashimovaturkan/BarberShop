using BarberShop.Application.Models.Dto.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.EntityCQ.File.Interfaces
{
    public interface IFileService : IBaseService
    {
        Task<UploadPhotoResponse> UploadPhoto(UploadPhotoRequest uploadPhotoRequest);
        Task<Domain.Photo> GetPhoto(int id);
    }
}
