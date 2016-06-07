using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Core
{
    public interface IUnitOfWork<U> where U : DbContext, IDisposable
    {
        int intCommit();
        Task<int> intCommitAsync();
        IEntityService<TEntity> GetEntityService<TEntity>() where TEntity : class;
    }
}
