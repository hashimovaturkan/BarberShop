using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Threading;
using System.Threading.Tasks;
using BarberShop.Domain;

namespace BarberShop.Persistence
{
    public class BarberShopDbContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserClaim> UserClaims { get; set; }
        public virtual DbSet<UserClaimType> UserClaimTypes { get; set; }
        public virtual DbSet<UserLog> UserLogs { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<UserRoleClaim> UserRoleClaims { get; set; }
        public virtual DbSet<UserRoleRelation> UserRoleRelations { get; set; }
        public virtual DbSet<UserStatus> UserStatuses { get; set; }
        public virtual DbSet<UserToken> UserTokens { get; set; }
        public virtual DbSet<UserTokenStatus> UserTokenStatuses { get; set; }
        public virtual DbSet<UserTokenType> UserTokenTypes { get; set; }
        public virtual DbSet<Filial> Filials { get; set; }
        public virtual DbSet<ErrorLog> ErrorLogs { get; set; }
        public virtual DbSet<Lang> Langs { get; set; }

        public BarberShopDbContext(DbContextOptions<BarberShopDbContext> options)
            : base(options) { }

        //public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        //{
        //    foreach (var entry in ChangeTracker.Entries())
        //    {
        //        switch (entry.State)
        //        {
        //            case EntityState.Added:
        //                var entity = entry.Entity;

        //                if (entity is ICreatedDate track)
        //                    track.CreatedDate = DateTime.UtcNow.AddHours(4);

        //                if (entity is IActive active)
        //                    active.IsActive = true;

        //                break;
        //        }
        //    }

        //    return base.SaveChangesAsync(cancellationToken);
        //}

        public override Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<TEntity> Update<TEntity>(TEntity entity)
        {
            var entry = Entry(entity).Entity;

            if (entry is IUpdatedDate track)
                track.UpdatedDate = DateTime.UtcNow.AddHours(4);

            return base.Update(entity);
        }

        public override Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<TEntity> Remove<TEntity>(TEntity entity)
        {
            var entry = Entry(entity).Entity;

            if (entry is IDeletedDate track)
                track.DeletedDate = DateTime.UtcNow.AddHours(4);

            if (entry is IActive active)
                active.IsActive = false;

            return base.Update(entry);
        }

    }

    public class DbInitializer
    {
        public static void Initialize(BarberShopDbContext context)
        {
            context.Database.EnsureCreated();
            context.Database.Migrate();
        }
    }
}
