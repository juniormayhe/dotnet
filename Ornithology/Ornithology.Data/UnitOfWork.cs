using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ornithology.Data
{
    public class UnitOfWork : OrnithologyDbContext, IUnitOfWork
    {
        public void ApplyCurrentValues<TEntity>(TEntity original, TEntity current) where TEntity : class
        {
            base.Entry<TEntity>(original).CurrentValues.SetValues(current);
        }

        public void Attach<TEntity>(TEntity item) where TEntity : class
        {
            base.Entry<TEntity>(item).State = EntityState.Unchanged;
        }

        public int Commit()
        {
            return base.SaveChanges();
        }

        public Task<int> CommitAsync()
        {
            return base.SaveChangesAsync();
        }

        public IDbSet<TEntity> CreateSet<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public void DetachEntities<TEntity>() where TEntity : class
        {
            base.Set<TEntity>().Local.ToList().ForEach(p => base.Entry(p).State = EntityState.Detached);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public TEntity Reload<TEntity>(TEntity item) where TEntity : class
        {
            if (item != null)
            {
                var context = ((IObjectContextAdapter)this).ObjectContext;

                context.Refresh(System.Data.Entity.Core.Objects.RefreshMode.StoreWins, item);

                this.Entry(item).Reload();

                var id = item.GetType().GetProperty("Codigo").GetValue(item);
                return base.Set<TEntity>().Find(id);
            }

            return item;
        }

        public void Reload<TEntity>() where TEntity : class
        {
            var context = ((IObjectContextAdapter)this).ObjectContext;

            context.Refresh(System.Data.Entity.Core.Objects.RefreshMode.StoreWins, context.ObjectStateManager.GetObjectStateEntries(EntityState.Unchanged | EntityState.Modified));
        }

        public void SetModified<TEntity>(TEntity item) where TEntity : class
        {
            try
            {
                var entityState = base.Entry<TEntity>(item).State;
                if (entityState == EntityState.Detached)
                {
                    var id = item.GetType().GetProperty("Codigo").GetValue(item);
                    ApplyCurrentValues<TEntity>(base.Set<TEntity>().Find(id), item);
                    base.Entry<TEntity>(base.Set<TEntity>().Find(id));
                }
                else
                {
                    base.Entry<TEntity>(item).State = EntityState.Modified;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Excepción no controlada en el método SetModified de QueryableUnitOfWork", ex);
            }
        }
    }
}
