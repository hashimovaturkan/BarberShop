
using BarberShop.Application.EntitiesCQ.User.Commands.CreateUser;
using BarberShop.Application.EntitiesCQ.User.Queries.PassResetGetUser;
using BarberShop.Application.EntitiesCQ.User.Queries.UserLogin;
using BarberShop.Application.Models.Template;
using BarberShop.Application.Models.Vm.User;
using System.Threading.Tasks;
using BarberShop.Application.Models.Dto.User;
using MediatR;

namespace BarberShop.Application.EntitiesCQ.User.Interfaces
{
    public interface IUserService : IBaseService
    {
        Task<UserClaimsVm> GetUserClaims(int userId);
        Task<UserLoginVm> LoginAsync(UserLoginQuery request);
        Task<bool> VerifyPhoneNumber(int? id, string value);
        Task<int> CreateAsync(CreateUserCommand createUserDto);
        Task<bool> SendSms(string phoneNumber);
    }
}