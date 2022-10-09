using BarberShop.Application.Models.Template;
using BarberShop.Application.Models.Vm.User;
using MediatR;

namespace BarberShop.Application.EntitiesCQ.User.Queries.PassResetGetUser
{
    public class PassResetGetUserQuery
    {
        public string Key { get; set; }
    }
}