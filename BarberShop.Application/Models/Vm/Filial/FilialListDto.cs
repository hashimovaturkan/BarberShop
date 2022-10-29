using BarberShop.Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.Models.Vm.Filial
{
    public class FilialListDto : IMapWith<Domain.Filial>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
