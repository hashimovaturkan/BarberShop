using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarberShop.Domain
{
    public class Lang : ShortTemplate
    {
        [Required]
        [Column(TypeName = "nvarchar")]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "varchar")]
        [MaxLength(5)]
        public string Shortname { get; set; }

    }
}
