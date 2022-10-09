using BarberShop.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarberShop.Domain
{
    public class UserRole : Template
    {
        public UserRole()
        {
            UserRoleClaims = new HashSet<UserRoleClaim>();
            UserRoleRelations = new HashSet<UserRoleRelation>();
        }

        [Required]
        [Column(TypeName = "nvarchar")]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "nvarchar")]
        [MaxLength(200)]
        public string? Description { get; set; }

        public virtual ICollection<UserRoleClaim> UserRoleClaims { get; set; }
        public virtual ICollection<UserRoleRelation> UserRoleRelations { get; set; }
        
    }
}
