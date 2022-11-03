using AutoMapper;
using BarberShop.Application.Common.Components;
using BarberShop.Application.EntitiesCQ.UserRole.Interfaces;
using BarberShop.Application.EntityCQ.Barber.Interfaces;
using BarberShop.Application.EntityCQ.File.Interfaces;
using BarberShop.Application.Models.Dto.File;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using static System.Net.Mime.MediaTypeNames;

namespace BarberShop.WebApi.Controllers
{
    public class FileController : BaseController
    {
        private const string _tempFolder = "uploads";
        private readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(), _tempFolder);
        private readonly IFileService _imageService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;
        private readonly IUserRoleService _userRoleService;

        public FileController(IFileService imageService, IConfiguration configuration, IMapper mapper, IWebHostEnvironment environment,
            IUserRoleService userRoleService)
        {
            _imageService = imageService;
            _environment = environment;
            (_configuration, _mapper) = (configuration, mapper);
            _userRoleService = userRoleService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> UploadPhoto([FromForm]UploadPhotoRequest request)
        {
            var response = await _imageService.UploadPhoto(request);
            return Ok(response);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<FileResult> GetPhoto(string cyrptedPhoto)
        {
            var photoId = GetIdFromEncryptedString(cyrptedPhoto);
            var photo = await _imageService.GetPhoto(photoId);
            var photofullPath = Path.Combine(_filePath, photo.Path);

            var str = System.IO.File.OpenRead(photofullPath);
            return File(str, Image.Jpeg);
        }

        private int GetIdFromEncryptedString(string cyrpted) => int.Parse(CryptHelper.Decrypt(HttpUtility.UrlDecode(cyrpted)).Split('-')[0]);

    }
}
