using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarberShop.Domain
{
    public class ErrorLog
    {
        [Key]
        public int Id { get; set; }
        public int? UserId { get; set; }
        [Required]
        [Column(TypeName = "nvarchar")]
        [MaxLength(4000)]
        public string LogText { get; set; }
        [Column(TypeName = "nvarchar")]
        [MaxLength(50)]
        public string Controller { get; set; }
        [Column(TypeName = "nvarchar")]
        [MaxLength(50)]
        public string Action { get; set; }
        [Required]
        [Column(TypeName = "nvarchar")]
        [MaxLength(200)]
        public string URL { get; set; }
        public int? CreatedIP { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual User User { get; set; }
    }
}
