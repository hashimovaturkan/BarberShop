using AutoMapper;
using BarberShop.Application.Common.Mappings;
using BarberShop.Application.Models.Vm.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.Models.Vm.Filial
{
    public class FilialDetailsVm : IMapWith<Domain.Filial>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Filial, FilialDetailsVm>();
        }
    }
}
