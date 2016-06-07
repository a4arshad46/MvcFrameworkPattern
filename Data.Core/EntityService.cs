using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Core
{
    public class EntityService<TEntity> : Disposable, IEntityService<TEntity> where TEntity : class
    {
        private readonly DbContext oDataBaseContext;
        private IDbSet<TEntity> oDataModelEntities;
        protected virtual IDbSet<TEntity> DbSet
        {
            get
            {
                if (this.oDataModelEntities == null)
                {
                    this.oDataModelEntities = this.oDataBaseContext.Set<TEntity>();
                }
                return this.oDataModelEntities;
            }
        }
        public virtual IQueryable<TEntity> Table
        {
            get
            {
                return this.DbSet;
            }
        }
        public EntityService(DbContext oDataBaseContextIntialization)
        {
            this.oDataBaseContext = oDataBaseContextIntialization;
        }
        public IQueryable<TEntity> GetAll()
        {
            return this.DbSet;
        }
        public TEntity GetById(int intId)
        {
            return this.DbSet.Find(new object[]
            {
                intId
            });
        }
        public void Insert(TEntity oEntity)
        {
            if (oEntity == null)
            {
                throw new ArgumentNullException("oEntity is Null");
            }
            this.DbSet.Add(oEntity);
        }
        public void Update(TEntity oEntity)
        {
            if (oEntity == null)
            {
                throw new ArgumentNullException("oEntity");
            }
            this.DbSet.Attach(oEntity);
            this.oDataBaseContext.Entry<TEntity>(oEntity).State = EntityState.Modified;
        }
        public void Delete(int intId)
        {
            TEntity byId = this.GetById(intId);
            if (byId == null)
            {
                throw new ArgumentNullException("oEntity is null");
            }
            this.DbSet.Remove(byId);
        }
        public void Delete(TEntity oEntity)
        {
            if (oEntity == null)
            {
                throw new ArgumentNullException("oEntity is null");
            }
            this.DbSet.Remove(oEntity);
        }
        public void Delete(Expression<Func<TEntity, bool>> where)
        {
            IEnumerable<TEntity> enumerable = this.DbSet.Where(where).AsEnumerable<TEntity>();
            foreach (TEntity current in enumerable)
            {
                this.DbSet.Remove(current);
            }
        }
        public TEntity Get(Expression<Func<TEntity, bool>> where)
        {
            return this.DbSet.Where(where).FirstOrDefault<TEntity>();
        }
        public IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> where)
        {
            return this.DbSet.Where(where).ToList<TEntity>();
        }
        public async Task<List<TEntity>> GetAllAsync()
        {
            return await this.DbSet.ToListAsync<TEntity>();
        }
        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> where)
        {
            return await this.DbSet.Where(where).FirstOrDefaultAsync<TEntity>();
        }
        public async Task<List<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> where)
        {
            return await this.DbSet.Where(where).ToListAsync<TEntity>();
        }
        protected override void vDisposeCore()
        {
            if (this.oDataBaseContext != null)
            {
                this.oDataBaseContext.Dispose();
            }
        }
        public List<TEntity> ExecuteStoredProcedureList<TEntity>(string sCommandText, params DbParameter[] oArrParameters) where TEntity : class
        {
            StringBuilder stringBuilder = new StringBuilder();
            string value = string.Empty;
            if (oArrParameters != null && oArrParameters.Length > 0)
            {
                for (int i = 0; i <= oArrParameters.Length - 1; i++)
                {
                    if (oArrParameters[i] == null)
                    {
                        throw new Exception("OracleParameter Is Null");
                    }
                    if (i == 0)
                    {
                        stringBuilder.AppendFormat("begin {0} (", sCommandText);
                        stringBuilder.AppendFormat(" :{0}, ", oArrParameters[i].ParameterName);
                    }
                    else
                    {
                        stringBuilder.AppendFormat(" :{0}, ", oArrParameters[i].ParameterName);
                    }
                }
            }
            value = stringBuilder.ToString().Trim().Substring(0, stringBuilder.ToString().Trim().Length - 1);
            stringBuilder = new StringBuilder();
            stringBuilder.Append(value);
            stringBuilder.Append("  ); end; ");
            return this.oDataBaseContext.Database.SqlQuery<TEntity>(stringBuilder.ToString(), oArrParameters).ToList<TEntity>();
        }
        public List<DbParameter> ExecuteStoredProcedureCommand(string sCommandText, params DbParameter[] oArrParameters)
        {
            StringBuilder stringBuilder = new StringBuilder();
            string value = string.Empty;
            if (oArrParameters != null && oArrParameters.Length > 0)
            {
                for (int i = 0; i <= oArrParameters.Length - 1; i++)
                {
                    if (oArrParameters[i] == null)
                    {
                        throw new Exception("OracleParameter Is Null");
                    }
                    if (i == 0)
                    {
                        stringBuilder.AppendFormat("begin {0} (", sCommandText);
                        stringBuilder.AppendFormat(" :{0}, ", oArrParameters[i].ParameterName);
                    }
                    else
                    {
                        stringBuilder.AppendFormat(" :{0}, ", oArrParameters[i].ParameterName);
                    }
                }
            }
            value = stringBuilder.ToString().Trim().Substring(0, stringBuilder.ToString().Trim().Length - 1);
            stringBuilder = new StringBuilder();
            stringBuilder.Append(value);
            stringBuilder.Append("  ); end; ");
            int num = this.oDataBaseContext.Database.ExecuteSqlCommand(stringBuilder.ToString(), oArrParameters);
            List<DbParameter> list = new List<DbParameter>();
            int num2 = oArrParameters.Length;
            for (int j = 0; j < num2; j++)
            {
                if (oArrParameters[j].Direction == ParameterDirection.Output || oArrParameters[j].Direction == ParameterDirection.InputOutput)
                {
                    list.Add(oArrParameters[j]);
                }
            }
            return list;
        }
        public string strExceptionLogger(DbEntityValidationException oDbEntityValidationException)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (DbEntityValidationResult current in oDbEntityValidationException.EntityValidationErrors)
            {
                foreach (DbValidationError current2 in current.ValidationErrors)
                {
                    stringBuilder.AppendFormat("Property: {0} Error: {1} {2}", current2.PropertyName, current2.ErrorMessage, Environment.NewLine);
                }
            }
            return stringBuilder.ToString();
        }
    }
}
