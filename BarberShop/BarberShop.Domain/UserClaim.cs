
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarberShop.Domain
{
    public class UserClaim : ShortTemplate
    {
        public UserClaim()
        {
            //Modules = new HashSet<Module>();
            UserRoleClaims = new HashSet<UserRoleClaim>();
        }

        public int UserClaimTypeId { get; set; }
        [Required]
        [Column(TypeName = "nvarchar")]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "nvarchar")]
        [MaxLength(100)]
        public string DisplayName { get; set; }
        [Required]
        [Column(TypeName = "nvarchar")]
        [MaxLength(100)]
        public string Description { get; set; }

        public virtual UserClaimType UserClaimType { get; set; }
        public virtual ICollection<UserRoleClaim> UserRoleClaims { get; set; }
    }
}
