using System.Text.Json.Serialization;

namespace BarberShop.Application.Models.Template
{
    public class RequestTemplate
    {
        [JsonIgnore]
        public int UserId { get; set; }
        [JsonIgnore]
        public string UserIp { get; set; }
    }
}
