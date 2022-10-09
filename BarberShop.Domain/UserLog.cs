using System;
using System.ComponentModel.DataAnnotations;

namespace BarberShop.Domain
{
    public class UserLog
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserIp { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}