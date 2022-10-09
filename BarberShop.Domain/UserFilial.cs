using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Domain
{
    public class UserFilial : Template
    {
        public int UserId { get; set; }
        public int FilialId { get; set; }

        public virtual User User { get; set; }
        public virtual Filial Filial { get; set; }
    }
}
