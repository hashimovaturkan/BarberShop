using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BarberShop.Application.Models.Dto.User
{
    public class UserLoginDto
    {
        public string Phone { get; set; }

        public string Password { get; set; }
    }
}
