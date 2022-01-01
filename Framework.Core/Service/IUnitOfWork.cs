using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Framework.Core.Service
{
    public interface IUnitOfWork
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        void RejectChanges();
        int SaveChanges(bool validateOnSaveEnabled, bool autoDetect = true, bool invalidateCacheDependencies = false);
        void SaveChangesAsync(bool validateOnSaveEnabled, bool invalidateCacheDependencies = false);
        void MarkAsChanged<TEntity>(TEntity entity) where TEntity : class;
    }
}
