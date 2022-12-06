using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Domain
{
    public class Balance : Template
    {
        public int UserId { get; set; }
        public double UserBalance { get; set; }
        public double Refund { get; set; }
        public string? PaymentMethod { get; set; }

        public virtual User User { get; set; }
    }
}
