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
        }


        [Required]
        [Column(TypeName = "nvarchar")]
        [MaxLength(500)]
        public string FullName { get; set; }

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
        public int? PhotoId { get; set; }
        public int? QrCodeId { get; set; }
        public int UserStatusId { get; set; }
        public int? FilialId { get; set; }

        public virtual ICollection<ErrorLog> ErrorLogs { get; set; }
        public virtual UserStatus UserStatus { get; set; }
        public virtual Filial Filial { get; set; }
        public virtual Photo Photo { get; set; }
        public virtual Photo QrCode { get; set; }
        public virtual ICollection<UserRoleRelation> UserRoleRelations { get; set; }
        public virtual ICollection<UserToken> UserTokens { get; set; }
    }
}