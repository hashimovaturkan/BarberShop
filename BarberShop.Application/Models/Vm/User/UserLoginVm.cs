using AutoMapper;
using BarberShop.Application.Common.Mappings;
using System.Linq;

namespace BarberShop.Application.Models.Vm.User
{
    public class UserLoginVm : IMapWith<Domain.User>
    {
        public int UserId { get; set; }
        public int FilialId { get; set; }
        public int LangId { get; set; } = 1;
        public string Email { get; set; } 
        public string Phone { get; set; } 
        public string FullName { get; set; } 
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.User, UserLoginVm>()
                .ForMember(userLoginVm => userLoginVm.UserId,
                    opt => opt.MapFrom(user => user.Id));
        }
    }

    public class UserLoginVmClaim
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
