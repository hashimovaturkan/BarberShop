
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Domain
{
    public class Filial : Template
    {
        public Filial()
        {
            UserFilials = new HashSet<UserFilial>();
        }
        public string Name { get; set; }

        public virtual ICollection<UserFilial> UserFilials { get; set; }
    }
}
