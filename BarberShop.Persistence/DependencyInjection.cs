using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BarberShop.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var conStr = configuration.GetConnectionString("connectionString");

            services.AddDbContext<BarberShopDbContext>(opt => opt.UseSqlServer(conStr));

            return services;
        }
    }
}
