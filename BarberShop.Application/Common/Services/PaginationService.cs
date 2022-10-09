using AutoMapper;
using BarberShop.Application.Common.Components;
using BarberShop.Application.Models.Template;
using BarberShop.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarberShop.Application.Common.Services
{
    internal class PaginationService
    {
        private readonly IMapper _mapper;
        private readonly UriService _uriService;

        public PaginationService(IMapper mapper, UriService uriService)
            => (_mapper, _uriService) = (mapper, uriService);

        public async Task<T2> GetPagedResponse<T1, T2>(
                IQueryable<Template> entityQuery, int pageNumber, int pageSize, string route, Func<List<T1>, int, int, T2> ctor)
            where T2 : ResponseListTemplate<List<T1>>
        {
            PaginationFilter paginationFilter = new PaginationFilter(pageNumber, pageSize);
            IQueryable<Template> entityPaged = paginationFilter.GetPagedList(entityQuery.OrderByDescending(e => e.CreatedDate));

            int totalRecords = await entityQuery.CountAsync();
            List<Template> entities = await entityPaged.ToListAsync() ?? new List<Template>();

            List<T1> entityVms = _mapper.Map<List<T1>>(entities);

            T2 respose = ctor(entityVms, paginationFilter.PageNumber, paginationFilter.PageSize);
            double totalPages = ((double)totalRecords / (double)paginationFilter.PageSize);
            int roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
            respose.NextPage =
                paginationFilter.PageNumber >= 1 && paginationFilter.PageNumber < roundedTotalPages
                ? _uriService.GetPageUri(new PaginationFilter(paginationFilter.PageNumber + 1, paginationFilter.PageSize), route)
                : null;
            respose.PreviousPage =
                paginationFilter.PageNumber - 1 >= 1 && paginationFilter.PageNumber <= roundedTotalPages
                ? _uriService.GetPageUri(new PaginationFilter(paginationFilter.PageNumber - 1, paginationFilter.PageSize), route)
                : null;
            respose.FirstPage = _uriService.GetPageUri(new PaginationFilter(1, paginationFilter.PageSize), route);
            respose.LastPage = _uriService.GetPageUri(new PaginationFilter(roundedTotalPages, paginationFilter.PageSize), route);
            respose.TotalPages = roundedTotalPages;
            respose.TotalRecords = totalRecords;

            return respose;
        }
    }
}
