using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarberShop.Domain
{
    public class User : Template
    {
        public User()
        {
            ErrorLogs = new HashSet<ErrorLog>();
            UserRoleRelations = new HashSet<UserRoleRelation>();
            UserTokens = new HashSet<UserToken>();
            UserFilials = new HashSet<UserFilial>();
        }


        [Required]
        [Column(TypeName = "nvarchar")]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "nvarchar")]
        [MaxLength(50)]
        public string Surname { get; set; }

        [Required]
        [Column(TypeName = "nvarchar")]
        [MaxLength(50)]
        public string Email { get; set; }
        public bool EmailVerification { get; set; }
        public bool PhoneVerification { get; set; }

        public string Phone { get; set; }

        [Required]
        [Column(TypeName = "varchar")]
        [MaxLength(100)]
        public string Password { get; set; }

        public Guid Salt { get; set; }
        public int UserStatusId { get; set; }

        public virtual ICollection<ErrorLog> ErrorLogs { get; set; }
        public virtual UserStatus UserStatus { get; set; }
        public virtual ICollection<UserRoleRelation> UserRoleRelations { get; set; }
        public virtual ICollection<UserToken> UserTokens { get; set; }
        public virtual ICollection<UserFilial> UserFilials { get; set; }
    }
}