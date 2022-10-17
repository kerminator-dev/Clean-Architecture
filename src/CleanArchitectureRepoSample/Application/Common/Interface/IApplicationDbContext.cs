using Domain.Master;
using Domain.Product;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interface
{
    public interface IApplicationDbContext
    {
        DbSet<AppSetting> AppSettings { get; set; }

        DbSet<Category> Categories { get; set; }  
        
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        Task<int> SaveChangesAsync();
    }
}
