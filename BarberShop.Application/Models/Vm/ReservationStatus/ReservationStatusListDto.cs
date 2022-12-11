using BarberShop.Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.Models.Vm.ReservationStatus
{
    public class ReservationStatusListVm : IMapWith<Domain.ReservationStatus>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
