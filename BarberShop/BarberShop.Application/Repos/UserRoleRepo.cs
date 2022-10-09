using BarberShop.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BarberShop.Application.Repos
{
    public class UserRoleRepo : BaseRepo<Domain.UserRole>
    {
        public UserRoleRepo(BarberShopDbContext dbContext) : base(dbContext) { }

        public IQueryable<Domain.UserRole> GetListQuery()
        {
            IQueryable<Domain.UserRole> userRoles = AsQueryable().AsNoTracking()
                .Where(e => e.IsActive);

            return userRoles;
        }

        public async Task<Domain.UserRole> GetByIdAsync(int roleId)
        {
            Domain.UserRole? userRole = await AsQueryable().AsNoTracking()
                .Where(e => e.Id == roleId && e.IsActive)
                .FirstOrDefaultAsync();

            return userRole;
        }

        public async Task<Domain.UserRole> GetByNameAsync(string roleName)
        {
            Domain.UserRole? userRole = await AsQueryable().AsNoTracking()
                .Where(e => e.Name == roleName && e.IsActive)
                .FirstOrDefaultAsync();

            return userRole;
        }

        public async Task<Domain.UserRole> GetByIdWithTrackingAsync(int roleId)
        {
            Domain.UserRole? userRole = await AsQueryable()
                .Include(e => e.UserRoleClaims.Where(f => f.IsActive && f.UserClaim.IsActive))
                    .ThenInclude(e => e.UserClaim)
                .Where(e => e.Id == roleId && e.IsActive)
                .FirstOrDefaultAsync();

            return userRole;
        }

        public async Task<Domain.UserRoleRelation> GetRelationAsync(int roleId, int userId)
        {
            Domain.UserRole? role = await AsQueryable().AsNoTracking()
                .Include(e => e.UserRoleRelations.Where(f => f.UserId == userId && f.IsActive && f.UserRole.IsActive))
                .Where(e => e.Id == roleId && e.IsActive)
                .FirstOrDefaultAsync();

            return role?.UserRoleRelations?.FirstOrDefault();
        }

        public async Task<Domain.UserRole> CreateUserRoleAsync(Domain.UserRole userRole, CancellationToken cancellationToken)
        {
            await DbContext.AddAsync(userRole);
            await DbContext.SaveChangesAsync(cancellationToken);

            return userRole;
        }

        public async Task CreateUserRoleRelationAsync(Domain.UserRoleRelation relation, CancellationToken cancellationToken)
        {
            await DbContext.AddAsync(relation);
            await DbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateUserRoleAsync(Domain.UserRole role, List<int> claimIds, string userIp, CancellationToken cancellationToken)
        {
            List<Domain.UserRoleClaim> roleClaims = role.UserRoleClaims.ToList();
            foreach (var rel in roleClaims.Where(e => !claimIds.Contains(e.UserClaimId)))
            {
                rel.DeletedDate = DateTime.Now;
                rel.IsActive = false;
            }

            roleClaims.AddRange(
                    claimIds
                        .Where(e => !roleClaims.Select(f => f.UserClaimId).Contains(e))
                        .Select(e => new Domain.UserRoleClaim { UserRole = role, UserClaimId = e, CreatedIp = userIp, CreatedDate = DateTime.Now })
                );

            role.UserRoleClaims = roleClaims;

            await DbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteUserRoleAsync(int roleId, CancellationToken cancellationToken)
        {
            Domain.UserRole role = await AsQueryable()
                .Include(e => e.UserRoleClaims)
                .Include(e => e.UserRoleRelations)
                .Where(e => e.Id == roleId && e.IsActive)
                .FirstOrDefaultAsync();

            role.IsActive = false;
            role.DeletedDate = DateTime.Now;
            foreach (var item in role.UserRoleClaims)
            {
                item.IsActive = false;
                item.DeletedDate = DateTime.Now;
            }
            foreach (var item in role.UserRoleRelations)
            {
                item.IsActive = false;
                item.DeletedDate = DateTime.Now;
            }

            await DbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteUserRoleRelationAsync(int roleId, int userId, CancellationToken cancellationToken)
        {
            Domain.UserRole role = await AsQueryable()
                .Include(e => e.UserRoleRelations)
                .Where(e => e.Id == roleId)
                .FirstOrDefaultAsync();

            foreach (var item in role.UserRoleRelations.Where(e => e.UserRoleId == roleId && e.UserId == userId && e.IsActive))
            {
                item.IsActive = false;
                item.DeletedDate = DateTime.Now;
            }

            await DbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
