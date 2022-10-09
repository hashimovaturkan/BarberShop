using BarberShop.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarberShop.Domain
{
    public class UserTokenStatus : ShortTemplate
    {
        public UserTokenStatus()
        {
            UserTokens = new HashSet<UserToken>();
        }

        [Required]
        [Column(TypeName = "nvarchar")]
        [MaxLength(50)]
        public string Name { get; set; }

        public virtual ICollection<UserToken> UserTokens { get; set; }
    }
}
