using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Domain
{
    public class Order: Template
    {
        public int UserId { get; set; }
        public int ServiceId { get; set; }
        public int AdditionalServiceId { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }


        public virtual User User { get; set; }
        public virtual Service Service { get; set; }
        public virtual AdditionalService AdditionalService { get; set; }
    }
}
