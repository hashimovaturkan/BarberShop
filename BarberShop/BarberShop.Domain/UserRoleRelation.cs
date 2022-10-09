using BarberShop.Domain;
using System;

namespace BarberShop.Domain
{
    public class UserRoleRelation : Template
    {
        public int UserId { get; set; }
        public int UserRoleId { get; set; }

        public virtual User User { get; set; }
        public virtual UserRole UserRole { get; set; }
    }
}
