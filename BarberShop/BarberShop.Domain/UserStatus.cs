using BarberShop.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarberShop.Domain
{
    public class UserStatus : ShortTemplate
    {
        public UserStatus()
        {
            Users = new HashSet<User>();
        }

        [Required]
        [Column(TypeName = "nvarchar")]
        [MaxLength(50)]
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
