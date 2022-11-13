
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
        public string Lang { get; set; }
        public string Long { get; set; }
        public string? Address { get; set; }
        public int? PhotoId { get; set; }
        public DateTime? OpenTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<User> Users { get; set; }
        public virtual Photo Photo { get; set; }
    }
}
