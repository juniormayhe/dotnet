using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Ornithology.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IDbSet<TEntity> CreateSet<TEntity>() where TEntity : class;

        void Attach<TEntity>(TEntity item) where TEntity : class;

        void SetModified<TEntity>(TEntity item) where TEntity : class;

        void ApplyCurrentValues<TEntity>(TEntity original, TEntity current) where TEntity : class;

        TEntity Reload<TEntity>(TEntity item) where TEntity : class;

        void Reload<TEntity>() where TEntity : class;

        void DetachEntities<TEntity>() where TEntity : class;

        int Commit();

        Task<int> CommitAsync();
    }
}