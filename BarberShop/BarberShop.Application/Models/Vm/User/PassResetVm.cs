using AutoMapper;
using BarberShop.Application.Common.Mappings;

namespace BarberShop.Application.Models.Vm.User
{
    public class PassResetVm : IMapWith<Domain.User>
    {
        public int UserId { get; set; }
        public string Email { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.User, PassResetVm>()
                .ForMember(passResetVm => passResetVm.UserId,
                    opt => opt.MapFrom(user => user.Id));
        }
    }
}
