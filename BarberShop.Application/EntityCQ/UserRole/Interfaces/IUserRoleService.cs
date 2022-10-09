
using System.Threading.Tasks;

namespace BarberShop.Application.EntitiesCQ.UserRole.Interfaces
{
    public interface IUserRoleService : IBaseService
    {
        Task AddRoleToUser(string name, int userId, string userIp);
    }
}
