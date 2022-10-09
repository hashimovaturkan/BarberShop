using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Persistence
{
    public static class BarberShopDbSeed
    {
        public static IApplicationBuilder Seed(this IApplicationBuilder builder)
        {
            using (var scope = builder.ApplicationServices.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<BarberShopDbContext>();
                db.Database.Migrate();
            }
            return builder;
        }
    }
}
