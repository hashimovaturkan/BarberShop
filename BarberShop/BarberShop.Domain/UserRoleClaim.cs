namespace BarberShop.Domain
{
    public class UserRoleClaim : Template
    {
        public int UserRoleId { get; set; }
        public int UserClaimId { get; set; }

        public virtual UserClaim UserClaim { get; set; }
        public virtual UserRole UserRole { get; set; }
    }
}
