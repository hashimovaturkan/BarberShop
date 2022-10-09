using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarberShop.Domain
{
    public class UserClaimType : ShortTemplate
    {
        public UserClaimType()
        {
            UserClaims = new HashSet<UserClaim>();
        }

        [Required]
        [Column(TypeName = "nvarchar")]
        [MaxLength(50)]
        public string Name { get; set; }

        public virtual ICollection<UserClaim> UserClaims { get; set; }
    }
}
