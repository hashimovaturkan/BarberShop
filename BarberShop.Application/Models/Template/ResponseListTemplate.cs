using System;

namespace BarberShop.Application.Models.Template
{
    public class ResponseListTemplate<T> : ResponseTemplate<T>
    {
        public int Number { get; set; }
        public int Size { get; set; }
        public Uri? FirstPage { get; set; }
        public Uri? LastPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public Uri? NextPage { get; set; }
        public Uri? PreviousPage { get; set; }

        public ResponseListTemplate(T data, int pageNumber, int pageSize/*, int totalRecords*/)
        {
            Number = pageNumber;
            Size = pageSize;
            //TotalPages = totalRecords;
            //TotalRecords = (int)Math.Ceiling((decimal)totalRecords / (decimal)pageSize);
            Data = data;
            Message = string.Empty;
            Succeeded = true;
            Errors = null;
        }
    }
}
