
using BarberShop.Domain;
using BarberShop.Persistence.Infrastructure;

namespace BarberShop.Persistence
{
    public abstract class BaseRepo<TEntity> : Repository<TEntity> where TEntity : BaseEntity, IActive
    {
        protected BaseRepo(BarberShopDbContext dbContext) : base(dbContext)
        {      
        }
    }
}
