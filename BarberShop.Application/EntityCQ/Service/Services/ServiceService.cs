using AutoMapper;
using BarberShop.Application.Common.Components;
using BarberShop.Application.Common.Extensions;
using BarberShop.Application.Common.Services;
using BarberShop.Application.EntityCQ.Service.Interfaces;
using BarberShop.Application.EntityCQ.Service.Queries;
using BarberShop.Application.Models.Template;
using BarberShop.Application.Models.Vm.Service;
using BarberShop.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.EntityCQ.Service.Services
{
    public class ServiceService : IServiceService
    {
        private readonly BarberShopDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly UriService _uriService;
        public ServiceService(BarberShopDbContext dbContext, IMapper mapper, UriService uriService)
        {
            this._uriService = uriService;
            this._dbContext = dbContext;
            this._mapper = mapper;
        }

        public async Task<ResponseListTemplate<List<ServiceLookupDto>>> GetList(GetServiceListQuery query)
        {
            var serviceQuery = _dbContext.Services.Where(e => e.IsActive);

            PaginationFilter paginationFilter = new PaginationFilter(query.Number, query.Size);
            IQueryable<Domain.Service> servicePagedQuery = paginationFilter.GetPagedList(serviceQuery);

            int totalRecords = await serviceQuery.CountAsync();

            var serviceLookupDtoList = _mapper.Map<List<ServiceLookupDto>>(servicePagedQuery);

            ResponseListTemplate<List<ServiceLookupDto>> result = serviceLookupDtoList.CreatePagedReponse(paginationFilter, totalRecords, _uriService, query.Route);

            return result;
        }
    }
}
