using System.Linq;

namespace BarberShop.Application.Common.Components
{
    public class PaginationFilter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public PaginationFilter()
        {
            PageNumber = 1;
            PageSize = 10;
        }
        public PaginationFilter(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize;
        }

        public IQueryable<T> GetPagedList<T>(IQueryable<T> dataList)
        {
            IQueryable<T> pagedDataList = dataList
                .Skip((PageNumber - 1) * PageSize)
                .Take(PageSize)
                .AsQueryable();

            return pagedDataList;
        }
    }
}
