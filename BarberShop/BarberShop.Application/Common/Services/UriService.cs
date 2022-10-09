using BarberShop.Application.Common.Components;
using Microsoft.AspNetCore.WebUtilities;
using System;

namespace BarberShop.Application.Common.Services
{
    public class UriService
    {
        private readonly string _baseUri;

        public UriService(string baseUri) => _baseUri = baseUri;

        public Uri GetPageUri(PaginationFilter filter, string route)
        {
            Uri enpointUri = new Uri(string.Concat(_baseUri, route));
            string modifiedUri = QueryHelpers.AddQueryString(enpointUri.ToString(), "pageNumber", filter.PageNumber.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", filter.PageSize.ToString());

            return new Uri(modifiedUri);
        }

        public Uri GetPassResetUri(string route, string key)
        {
            Uri endpointUri = new Uri(string.Concat(route,"auth/activation"));
            string modifiedUri = QueryHelpers.AddQueryString(endpointUri.ToString(), "key", key);

            return new Uri(modifiedUri);
        }
    }
}
