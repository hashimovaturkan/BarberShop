
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
            Users = new HashSet<User>();
        }
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
