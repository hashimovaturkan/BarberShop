using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Domain
{
    public class UserGiftRelation : Template
    {
        public int UserId { get; set; }
        public int GiftId { get; set; }
        public bool Status { get; set; }

        public virtual User User { get; set; }
        public virtual Gift Gift { get; set; }
    }
}
