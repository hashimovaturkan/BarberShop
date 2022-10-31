using AutoMapper;
using BarberShop.Application.Models.Template;
using Microsoft.AspNetCore.Http;

namespace BarberShop.Application.EntitiesCQ.User.Commands.CreateUser
{
    public class CreateUserCommand : RequestTemplate
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public int FilialId { get; set; }
        public string Email { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateUserCommand, Domain.User>()
                .ForMember(emp => emp.CreatedIp,
                    opt => opt.MapFrom(employeeContactCreateDto => employeeContactCreateDto.UserIp));
        }
    }
}