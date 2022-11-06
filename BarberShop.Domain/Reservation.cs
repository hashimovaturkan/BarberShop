using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Domain
{
    public class Reservation : Template
    {
        public DateTime ReservationDate { get; set; }

        [ForeignKey("Service")]
        public int? FirstServiceId { get; set; }
        [ForeignKey("Service")]
        public int? SecondServiceId { get; set; }
        public int FilialId { get; set; }
        public int UserId { get; set; }
        public int ReservationStatusId { get; set; }

        public virtual Service FirstService { get; set; }
        public virtual Service SecondService { get; set; }
        public virtual Filial Filial { get; set; }
        public virtual User User { get; set; }
        public virtual ReservationStatus ReservationStatus { get; set; }
    }
}
