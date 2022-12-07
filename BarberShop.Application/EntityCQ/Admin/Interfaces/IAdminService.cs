using BarberShop.Application.EntitiesCQ.User.Commands.CreateUser;
using BarberShop.Application.EntitiesCQ.User.Queries.UserLogin;
using BarberShop.Application.EntityCQ.Admin.Commands;
using BarberShop.Application.EntityCQ.User.Commands.UpdateUser;
using BarberShop.Application.EntityCQ.User.Queries.UserList;
using BarberShop.Application.Models.Dto.Mail;
using BarberShop.Application.Models.Vm.Admin;
using BarberShop.Application.Models.Vm.Roles;
using BarberShop.Application.Models.Vm.User;
using IntraNet.Application.Models.Vm.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.EntityCQ.Admin.Interfaces
{
    public interface IAdminService : IBaseService
    {
        Task<AdminLoginVm> LoginAsync(UserLoginQuery request);
        Task<int> CreateAsync(CreateAdminCommand createUserDto);
        Task<List<RoleVm>> GetRoles();
        Task<AdminDetailsVm> Get(int userId);
        Task<int> Update(UpdateAdminCommand userDto);


    }
}
