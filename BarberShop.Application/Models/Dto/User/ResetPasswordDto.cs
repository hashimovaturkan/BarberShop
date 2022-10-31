using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.Models.Dto.User
{
    public class ResetPasswordDto
    {
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
    }
}
