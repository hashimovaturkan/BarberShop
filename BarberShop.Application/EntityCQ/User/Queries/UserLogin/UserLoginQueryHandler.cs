using AutoMapper;
using BarberShop.Application.Common.Mappings;
using BarberShop.Application.Models.Dto.User;
using System.ComponentModel.DataAnnotations;

namespace BarberShop.Application.EntitiesCQ.User.Queries.UserLogin
{
    public class UserLoginQuery : IMapWith<UserLoginDto>
    {
        [Required(ErrorMessage = "Phone is required")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public int LangId { get; set; } = 1;
        public string UserIp { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UserLoginDto, UserLoginQuery>();
        }
    }
}