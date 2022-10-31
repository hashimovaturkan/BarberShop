using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.Models.Dto.User
{
    public class SmsVerificationDto
    {
        public string PhoneNumber { get; set; }
        public string Value { get; set; }

    }
}
