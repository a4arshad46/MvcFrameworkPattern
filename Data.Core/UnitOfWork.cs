using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Core
{
    public class UnitOfWork<TContext> : Disposable, IUnitOfWork<TContext> where TContext : DbContext, new()
    {
        protected readonly DbContext oDbContext;
        protected Dictionary<Type, object> dicServices;
        public IEntityService<T> GetEntityService<T>() where T : class
        {
            IEntityService<T> result;
            if (this.dicServices.Keys.Contains(typeof(T)))
            {
                result = (this.dicServices[typeof(T)] as IEntityService<T>);
            }
            else
            {
                IEntityService<T> services = new EntityService<T>(this.oDbContext);
                this.dicServices.Add(typeof(T), services);
                result = services;
            }
            return result;
        }
        public UnitOfWork()
        {
            this.oDbContext = Activator.CreateInstance<TContext>();
            this.dicServices = new Dictionary<Type, object>();
        }
        public virtual Task<int> intCommitAsync()
        {
            return this.oDbContext.SaveChangesAsync();
        }
        public virtual int intCommit()
        {
            return this.oDbContext.SaveChanges();
        }
        protected override void vDisposeCore()
        {
            if (this.oDbContext != null)
            {
                this.oDbContext.Dispose();
            }
        }
    }
}
