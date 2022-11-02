using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Domain
{
    public class Barber : Template
    {
        public string ImageUrl { get; set; }
        public string Name { get; set; }

        public int Priority { get; set; }
    }
}
