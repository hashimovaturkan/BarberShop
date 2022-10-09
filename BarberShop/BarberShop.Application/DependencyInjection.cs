using FluentValidation;
using BarberShop.Persistence;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;
using BarberShop.Persistence.Infrastructure;
using BarberShop.Application;

namespace BarberShop.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblies(new[] {Assembly.GetExecutingAssembly()});
            services.AddRepos().AddServices();
            
            return services;
        }

        private static IServiceCollection AddRepos(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            Assembly.GetExecutingAssembly().GetExportedTypes()
                .Where(e => !e.IsAbstract
                            && e.BaseType is not null
                            && e.BaseType.IsGenericType
                            && e.BaseType.GetGenericTypeDefinition() == typeof(BaseRepo<>))
                .ToList()
                .ForEach(x => services.AddScoped(x));


            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            Assembly.GetExecutingAssembly().GetExportedTypes()
                .Where(e =>
                    e.IsClass
                    && !e.IsAbstract
                    && e.GetInterfaces().Contains(typeof(IBaseService)))
                .ToList()
                .ForEach(type =>
                {
                    var nestedInterface =type.GetInterfaces().First(x => x.GetInterfaces().Contains(typeof(IBaseService)));
                    services.AddScoped(nestedInterface, type);
                });
            return services;
        }
    }
}