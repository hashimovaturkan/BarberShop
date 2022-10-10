using BarberShop.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarberShop.Application.Repos
{
    public class UserRepo : BaseRepo<Domain.User>
    {
        public UserRepo(BarberShopDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Domain.User> GetVerifiedUserByIdAsync(int userId)
        {
            Domain.User? user = await AsQueryable().AsNoTracking()
                .Include(e => e.UserTokens)
                .Where(e => e.Id == userId && e.IsActive && e.PhoneVerification)
                .FirstOrDefaultAsync();

            return user;
        }

        public async Task<Domain.User> GetUnverifiedUserByIdAsync(int userId)
        {
            Domain.User? user = await AsQueryable().AsNoTracking()
                .Include(e => e.UserTokens)
                .Where(e => e.Id == userId && e.IsActive)
                .FirstOrDefaultAsync();

            return user;
        }

        public IQueryable<Domain.User> GetListQuery()
        {
            IQueryable<Domain.User> result = AsQueryable().AsNoTracking()
                .Include(e => e.UserFilials)
                .Include(e => e.UserRoleRelations)
                .Where(e => e.IsActive && e.PhoneVerification);

            return result;
        }


        public async Task<List<Domain.UserRole>> GetRolesListByUserIdQuery(int userId)
        {
            var roles = await AsQueryable().AsNoTracking()
                .Include(e => e.UserRoleRelations)
                .ThenInclude(e => e.UserRole)
                .Where(e => e.Id == userId && e.IsActive && e.PhoneVerification)
                .SelectMany(e => e.UserRoleRelations.Select(k => k.UserRole))
                .ToListAsync();

            return roles;
        }
    }
}