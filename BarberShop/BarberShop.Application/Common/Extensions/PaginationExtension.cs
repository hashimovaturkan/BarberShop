using BarberShop.Application.Common.Components;
using BarberShop.Application.Common.Services;
using BarberShop.Application.Models.Template;
using System;
using System.Collections.Generic;

namespace BarberShop.Application.Common.Extension
{
    public static partial class Extension
    {
        public static ResponseListTemplate<List<T>> CreatePagedReponse<T>(this List<T> pagedData, PaginationFilter validFilter, int totalRecords, UriService uriService, string route)
        {
            ResponseListTemplate<List<T>> respose = new ResponseListTemplate<List<T>>(pagedData, validFilter.PageNumber, validFilter.PageSize);
            double totalPages = ((double)totalRecords / (double)validFilter.PageSize);
            int roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
            respose.NextPage =
                validFilter.PageNumber >= 1 && validFilter.PageNumber < roundedTotalPages
                ? uriService.GetPageUri(new PaginationFilter(validFilter.PageNumber + 1, validFilter.PageSize), route)
                : null;
            respose.PreviousPage =
                validFilter.PageNumber - 1 >= 1 && validFilter.PageNumber <= roundedTotalPages
                ? uriService.GetPageUri(new PaginationFilter(validFilter.PageNumber - 1, validFilter.PageSize), route)
                : null;
            respose.FirstPage = uriService.GetPageUri(new PaginationFilter(1, validFilter.PageSize), route);
            respose.LastPage = uriService.GetPageUri(new PaginationFilter(roundedTotalPages, validFilter.PageSize), route);
            respose.TotalPages = roundedTotalPages;
            respose.TotalRecords = totalRecords;

            return respose;
        }
    }
}
