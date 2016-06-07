using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Core
{
    public interface IEntityService<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Table
        {
            get;
        }
        IQueryable<TEntity> GetAll();
        TEntity GetById(int intId);
        void Insert(TEntity oEntity);
        void Update(TEntity oEntity);
        void Delete(int intId);
        void Delete(TEntity oEntity);
        void Delete(Expression<Func<TEntity, bool>> where);
        TEntity Get(Expression<Func<TEntity, bool>> where);
        IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> where);
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> where);
        Task<List<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> where);
        List<TEntity> ExecuteStoredProcedureList<TEntity>(string sCommandText, params DbParameter[] oArrParameters) where TEntity : class;
    }
}
