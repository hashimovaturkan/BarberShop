using AutoMapper;
using BarberShop.Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.Models.Vm.Service
{
    public class ServiceLookupDto : IMapWith<Domain.Service>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double? Price { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Service, ServiceLookupDto>();
        }
    }
}
