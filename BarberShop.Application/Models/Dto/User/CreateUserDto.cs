using AutoMapper;
using BarberShop.Application.Common.Mappings;
using BarberShop.Application.EntitiesCQ.User.Commands.CreateUser;
using Microsoft.AspNetCore.Http;

namespace BarberShop.Application.Models.Dto.User
{
    public class CreateUserDto : IMapWith<CreateUserCommand>
    {
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public int FilialId { get; set; }
        public string Email { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateUserDto, CreateUserCommand>()
                .ForMember(createUserCommand => createUserCommand.Email, opt => opt.MapFrom(createUserDto => createUserDto.Email.Trim().ToLower()));
        }
    }
}
