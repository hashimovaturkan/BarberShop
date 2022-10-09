using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BarberShop.Application.Models.Dto.User
{
    public class UserLoginDto
    {
        [Required(ErrorMessage = "Phone is required")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
