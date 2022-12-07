using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Domain
{
    public class Admin : Template
    {
        [Required]
        [Column(TypeName = "nvarchar")]
        [MaxLength(500)]
        public string FullName { get; set; }

        [Required]
        [Column(TypeName = "nvarchar")]
        [MaxLength(50)]
        public string Email { get; set; }

        public string Phone { get; set; }

        [Required]
        [Column(TypeName = "varchar")]
        [MaxLength(100)]
        public string Password { get; set; }
        public int Role { get; set; }

        public int? PhotoId { get; set; }

        public virtual Photo Photo { get; set; }

    }
}
