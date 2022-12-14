using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Domain
{
    public class Barber : Template
    {
        public int? PhotoId { get; set; }
        public string Name { get; set; }
        public int? FilialId { get; set; }

        public int Priority { get; set; }
        public string? Description { get; set; }
        public virtual Photo Photo { get; set; }
        public virtual Filial Filial { get; set; }
    }
}
