using AutoMapper;
using BarberShop.Application.Common.Mappings;
using System.Collections.Generic;
using System.Linq;

namespace BarberShop.Application.Models.Vm.User
{
    public class UserClaimsVm : IMapWith<Domain.User>
    {
        public List<UserClaimVm> ClaimList { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.User, UserClaimsVm>()
                .ForMember(vm => vm.ClaimList,
                    opt => opt.MapFrom(user => user.UserRoleRelations
                        .Select(e => e.UserRole.UserRoleClaims
                            .Select(f => new UserClaimVm
                            {
                                ClaimId = f.UserClaim.Id,
                                ClaimName = f.UserClaim.Name,
                                DisplayName = f.UserClaim.DisplayName
                            }))
                        .SelectMany(e => e).ToList()));
            // .AfterMap((user, userClaimsVm, resContext) =>
            // {
            //     List<UserClaimVm> userClaimList = user.UserRoleRelations
            //         .Select(e => e.UserRole.UserRoleClaims
            //             .Select(f => new UserClaimVm { ClaimId = f.UserClaim.Id, ClaimName = f.UserClaim.Name }))
            //         .SelectMany(e => e).ToList();
            //     userClaimsVm = new UserClaimsVm { ClaimList = userClaimList };
            // });
        }
    }

    public class UserClaimVm
    {
        public int ClaimId { get; set; }
        public string ClaimName { get; set; }
        public string DisplayName { get; set; }
    }
}