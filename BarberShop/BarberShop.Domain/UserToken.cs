using BarberShop.Domain;
using System;

namespace BarberShop.Domain
{
    public class UserToken : Template
    {
        public int UserId { get; set; }
        public string Value { get; set; }
        public int UserTokenTypeId { get; set; }
        public int UserTokenStatusId { get; set; }
        public DateTime ExpireDate { get; set; }

        public virtual User User { get; set; }
        public virtual UserTokenStatus UserTokenStatus { get; set; }
        public virtual UserTokenType UserTokenType { get; set; }
    }
}
