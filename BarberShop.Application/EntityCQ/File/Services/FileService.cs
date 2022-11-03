using AutoMapper;
using BarberShop.Application.Common.Components;
using BarberShop.Application.Common.Services;
using BarberShop.Application.EntityCQ.File.Interfaces;
using BarberShop.Application.EntityCQ.File.Interfaces;
using BarberShop.Application.Models.Dto.File;
using BarberShop.Domain;
using BarberShop.Persistence;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BarberShop.Application.EntityCQ.File.Services
{
    public class FileService : IFileService
    {
        private readonly BarberShopDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;
        private readonly UriService _uriService;
        readonly IHttpContextAccessor httpContextAccessor;
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;
        private static string baseUrl;
        public const string Getphoto = Base + "/file/GetPhoto/{cyrptedPhoto}";
        public FileService(IHttpContextAccessor httpContextAccessor,BarberShopDbContext dbContext, IMapper mapper, IWebHostEnvironment environment, UriService uriService)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
            this._environment = environment;
            this._uriService = uriService;
            this.httpContextAccessor = httpContextAccessor;
            baseUrl = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host.ToUriComponent()}";
        }

        public async Task<Photo> GetPhoto(int id)
        {
            return await _dbContext.Photos.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<UploadPhotoResponse> UploadPhoto(UploadPhotoRequest request)
        {
            UploadPhotoResponse response = new();

            Domain.Photo photo = new()
            {
                Name = request.Photo.FileName,
                Path = request.Photo.SaveFileToFolderAndGetPath(),
                CreatedDate = DateTime.UtcNow,
                CreatedIp = "::1"
            };

            await _dbContext.Photos.AddAsync(photo);
            var created = await _dbContext.SaveChangesAsync();

            response.Url = GeneratePhotoUrl(photo.Id);
            return response;
        }

        public string GeneratePhotoUrl(int photoId)
        {
            if (photoId == 0)
            {
                return string.Empty;
            }
            return baseUrl + "/" + Getphoto.Replace("{cyrptedPhoto}", HttpUtility.UrlEncode(CryptHelper.Encrypt($"{photoId}")));
        }
    }
}
