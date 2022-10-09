using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarberShop.Domain
{
    public class Template :BaseEntity, ITrack
    {
        public bool IsActive { get; set; }

        [Required]
        [Column(TypeName = "varchar")]
        [MaxLength(20)]
        public string CreatedIp { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
       
    }

    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
    

    public class ShortTemplate :BaseEntity, IActive
    {
        public bool IsActive { get; set; }
    }

    

    public interface ITrack : IIPTrack, ICreatedDate, IUpdatedDate, IDeletedDate, IActive
    {
    }

    public interface IIPTrack
    {
        string CreatedIp { get; set; }
    }
    public interface ICreatedDate
    {
        DateTime CreatedDate { get; set; }
    }

    public interface IUpdatedDate
    {
        DateTime? UpdatedDate { get; set; }
    }

    public interface IDeletedDate
    {
        DateTime? DeletedDate { get; set; }
    }

    public interface IActive
    {
        bool IsActive { get; set; }
    }
}
