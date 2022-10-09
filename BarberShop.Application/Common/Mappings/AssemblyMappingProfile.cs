using AutoMapper;
using System.Reflection;

namespace BarberShop.Application.Common.Mappings
{
    public class AssemblyMappingProfile : Profile
    {
        public AssemblyMappingProfile(Assembly assembly)
        {
            ApplyMappingFromAssembly(assembly);
        }

        private void ApplyMappingFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(type => type.GetInterfaces()
                    .Any(e => e.IsGenericType
                    && e.GetGenericTypeDefinition() == typeof(IMapWith<>))
                ).ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);
                var methInfo = type.GetMethod("Mapping")
                     ?? type.GetInterface("IMapWith`1").GetMethod("Mapping");
                methInfo?.Invoke(instance, new object[] { this });
            }

            //CreateMap(typeof(ICollection<>), typeof(List<>)).ReverseMap();
        }
    }
}
