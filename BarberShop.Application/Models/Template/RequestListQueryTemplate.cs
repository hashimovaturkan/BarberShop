namespace BarberShop.Application.Models.Template
{
    public class RequestListQueryTemplate : RequestTemplate
    {
        public int Size { get; set; } = 20;
        public int Number { get; set; } = 1;
        public string Route { get; set; }
    }
}
