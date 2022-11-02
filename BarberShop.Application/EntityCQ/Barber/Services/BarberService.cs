using AutoMapper;
using BarberShop.Application.Common.Components;
using BarberShop.Application.Common.Extensions;
using BarberShop.Application.Common.Services;
using BarberShop.Application.EntityCQ.Barber.Commands.CreateBarber;
using BarberShop.Application.EntityCQ.Barber.Interfaces;
using BarberShop.Application.EntityCQ.Barber.Queries;
using BarberShop.Application.Models.Template;
using BarberShop.Application.Models.Vm.Barber;
using BarberShop.Application.Models.Vm.Filial;
using BarberShop.Application.Models.Vm.User;
using BarberShop.Domain;
using BarberShop.Persistence;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.EntityCQ.Barber.Services
{
    public class BarberService : IBarberService
    {
        private readonly BarberShopDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;
        private readonly UriService _uriService;
        public BarberService(BarberShopDbContext dbContext, IMapper mapper, IWebHostEnvironment environment, UriService uriService)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
            this._environment = environment;
            this._uriService = uriService;
        }

        public async Task<int> Create(CreateBarberCommand dto)
        {
            Domain.Barber barber = _mapper.Map<Domain.Barber>(dto);

            if (dto.Image != null)
                barber.ImageUrl = await dto.Image.FileUpload(_environment);

            await _dbContext.Barbers.AddAsync(barber, CancellationToken.None);
            await _dbContext.SaveChangesAsync(CancellationToken.None);

            return barber.Id;
        }

        public async Task<ResponseListTemplate<List<BarberListDto>>> GetList(GetBarberListQuery query, string route)
        {
            var barbers = _dbContext.Barbers.Where(e => e.IsActive);

            foreach (var barber in barbers)
            {
                barber.ImageUrl = barber.ImageUrl.GetFile(_environment);
            }

            PaginationFilter paginationFilter = new PaginationFilter(query.PageNumber, query.PageSize);
            IQueryable<Domain.Barber> surveyPagedQuery = paginationFilter.GetPagedList(barbers);

            int totalRecords = await barbers.CountAsync();
            List<Domain.Barber> surveyPaged = await surveyPagedQuery.OrderByDescending(e => e.CreatedDate).ToListAsync();

            List<BarberListDto> surveyLookupDtoList = _mapper.Map<List<BarberListDto>>(surveyPaged);


            ResponseListTemplate<List<BarberListDto>> result = surveyLookupDtoList.CreatePagedReponse(paginationFilter, totalRecords, _uriService, route);

            return result;
        }
    }
}
