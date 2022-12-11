
using BarberShop.Application.EntitiesCQ.User.Commands.CreateUser;
using BarberShop.Application.EntitiesCQ.User.Queries.UserLogin;
using BarberShop.Application.Models.Template;
using BarberShop.Application.Models.Vm.User;
using System.Threading.Tasks;
using BarberShop.Application.Models.Dto.User;
using MediatR;
using IntraNet.Application.Models.Vm.User;
using BarberShop.Application.EntityCQ.User.Commands.UpdateUser;
using BarberShop.Application.EntityCQ.User.Queries.UserList;
using BarberShop.Application.Models.Dto.Mail;

namespace BarberShop.Application.EntitiesCQ.User.Interfaces
{
    public interface IUserService : IBaseService
    {
        Task<UserClaimsVm> GetUserClaims(int userId);
        Task<UserLoginVm> LoginAsync(UserLoginQuery request);
        Task<bool> VerifyPhoneNumber(string phoneNumber, string value);
        Task<int> ResetPassword(string phoneNumber, string password);
        Task<int> CreateAsync(CreateUserCommand createUserDto);
        Task<bool> SendSms(string phoneNumber);
        Task<bool> SendMail(SendMailDto mailDto, int userId);

        Task<ResponseListTemplate<List<UserListVm>>> GetList(GetUserListQuery query);
        Task<UserDetailsVm> Get(int userId);
        Task<int> Update(UpdateUserCommand userDto);
    }
}