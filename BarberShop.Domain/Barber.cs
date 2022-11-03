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

        public int Priority { get; set; }
        public virtual Photo Photo { get; set; }
    }
}
