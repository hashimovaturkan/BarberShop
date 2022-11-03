using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Domain
{
    public class Photo : Template
    {
        public string Name { get; set; }
        public string Path { get; set; }
    }
}
