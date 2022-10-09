namespace BarberShop.Application.Models.Template
{
    public class RequestListQueryTemplate : RequestTemplate
    {
        public int PageSize { get; set; } = 20;
        public int PageNumber { get; set; } = 1;
        public string Route { get; set; }
    }
}
