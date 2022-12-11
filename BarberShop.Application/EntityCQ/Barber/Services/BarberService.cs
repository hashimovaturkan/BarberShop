using AutoMapper;
using BarberShop.Application.Common.Components;
using BarberShop.Application.Common.Exceptions;
using BarberShop.Application.Common.Extensions;
using BarberShop.Application.Common.Services;
using BarberShop.Application.EntityCQ.Barber.Commands.CreateBarber;
using BarberShop.Application.EntityCQ.Barber.Commands.UpdateBarber;
using BarberShop.Application.EntityCQ.Barber.Interfaces;
using BarberShop.Application.EntityCQ.Barber.Queries;
using BarberShop.Application.Models.Template;
using BarberShop.Application.Models.Vm.Barber;
using BarberShop.Application.Models.Vm.Filial;
using BarberShop.Application.Models.Vm.User;
using BarberShop.Domain;
using BarberShop.Persistence;
using BarberShop.Persistence.Migrations;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photo = BarberShop.Domain.Photo;

namespace BarberShop.Application.EntityCQ.Barber.Services
{
    public class BarberService : IBarberService
    {
        private readonly BarberShopDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;
        private readonly UriService _uriService;
        readonly IHttpContextAccessor httpContextAccessor;
        public BarberService(IHttpContextAccessor httpContextAccessor,BarberShopDbContext dbContext, IMapper mapper, IWebHostEnvironment environment, UriService uriService)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
            this._environment = environment;
            this._uriService = uriService;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<int> Create(CreateBarberCommand dto)
        {
            Domain.Barber barber = _mapper.Map<Domain.Barber>(dto);

            if (dto.Image != null)
            {
                Domain.Photo photo = new Domain.Photo();

                var image = dto.Image.ConvertFile();

                photo = new()
                {
                    Name = image.File.FileName,
                    Path = image.Path,
                    CreatedDate = DateTime.UtcNow,
                    CreatedIp = "::1"
                };

                barber.Photo = photo;
            }
                

            await _dbContext.Barbers.AddAsync(barber, CancellationToken.None);
            await _dbContext.SaveChangesAsync(CancellationToken.None);

            return barber.Id;
        }

        public async Task<ResponseListTemplate<List<BarberListDto>>> GetSearchList(GetBarberListQuery query,string? searchWord, string route)
        {
            var barbers = _dbContext.Barbers.Include(e => e.Photo).Where(e => e.IsActive);

            if (searchWord != null || searchWord != "")
                barbers = barbers.Where(e => e.Name.ToLower().Contains(searchWord.ToLower()));

            PaginationFilter paginationFilter = new PaginationFilter(query.PageNumber, query.PageSize);
            IQueryable<Domain.Barber> surveyPagedQuery = paginationFilter.GetPagedList(barbers);

            int totalRecords = await barbers.CountAsync();
            List<Domain.Barber> surveyPaged = await surveyPagedQuery.OrderByDescending(e => e.Priority).ToListAsync();

            List<BarberListDto> surveyLookupDtoList = _mapper.Map<List<BarberListDto>>(surveyPaged);
            foreach (var barber in surveyLookupDtoList)
            {
                if (barber.PhotoId != null)
                    barber.ImageUrl = httpContextAccessor.GeneratePhotoUrl((int)barber.PhotoId);

            }

            ResponseListTemplate<List<BarberListDto>> result = surveyLookupDtoList.CreatePagedReponse(paginationFilter, totalRecords, _uriService, route);

            return result;
        }

        public async Task<ResponseListTemplate<List<BarberListDto>>> GetList(GetBarberListQuery query, string route)
        {
            var barbers = _dbContext.Barbers.Include(e => e.Photo).Include(e => e.Filial).Where(e => e.IsActive);

            PaginationFilter paginationFilter = new PaginationFilter(query.PageNumber, query.PageSize);
            IQueryable<Domain.Barber> surveyPagedQuery = paginationFilter.GetPagedList(barbers);

            int totalRecords = await barbers.CountAsync();
            List<Domain.Barber> surveyPaged = await surveyPagedQuery.OrderByDescending(e => e.Priority).ToListAsync();

            List<BarberListDto> surveyLookupDtoList = _mapper.Map<List<BarberListDto>>(surveyPaged);
            foreach (var barber in surveyLookupDtoList)
            {
                if (barber.PhotoId != null)
                    barber.ImageUrl = httpContextAccessor.GeneratePhotoUrl((int)barber.PhotoId);

            }

            ResponseListTemplate<List<BarberListDto>> result = surveyLookupDtoList.CreatePagedReponse(paginationFilter, totalRecords, _uriService, route);

            return result;
        }

        public async Task<int> Update(UpdateBarberCommand dto)
        {
            var barber = await _dbContext.Barbers.Include(e => e.Filial).Include(e => e.Photo).FirstOrDefaultAsync(e => e.Id == dto.Id && e.IsActive);
            if (barber == null)
                throw new NotFoundException(nameof(Barber), barber.Id);

            var imageId = barber.PhotoId;

            _mapper.Map(dto, barber);
            barber.UpdatedDate = DateTime.UtcNow.AddHours(4);
            barber.PhotoId = imageId;

            if (dto.Image != null)
            {
                Photo photo = new Photo();
                var image = dto.Image.ConvertFile();

                photo = new()
                {
                    Name = image.File.FileName,
                    Path = image.Path,
                    CreatedDate = DateTime.UtcNow,
                    CreatedIp = "::1"
                };
                barber.Photo = photo;
            }

            await _dbContext.SaveChangesAsync(CancellationToken.None);

            return barber.Id;
        }
    }
}
