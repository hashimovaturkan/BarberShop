using AutoMapper;
using BarberShop.Application.Common.Mappings;
using BarberShop.Domain;
using System.Collections.Generic;

namespace IntraNet.Application.Models.Vm.User
{
    public class UserDetailsVm : IMapWith<BarberShop.Domain.User>
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string Email { get; set; }
        public byte UserSatatusId { get; set; }
        public UserStatus UserStatus { get; set; }
        public List<UserRoleRelation> UserRoleRelations { get; set; }
        public List<UserToken> UserTokens { get; set; }

        public void Mapping(Profile profile)
        {
            //profile.CreateMap<Domain.User, UserLookupDto>()
            //    .ForMember(userDto => userDto.UserRoleRelations,
            //        opt => opt.MapFrom(user => user.UserRoleRelations.ToList()))
            //    .ForMember(userDto => userDto.UserTokens,
            //        opt => opt.MapFrom(user => user.UserTokens.ToList()));
        }
    }
}
