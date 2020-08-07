using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Vehicle.Common;
using Vehicle.Common.PagingFilteringSorting;
using Vehicle.Model;
using Vehicle.Model.Common;

namespace Vehicle.Repository.Common
{
    public interface IRepository<TEntity> where TEntity : class
    {

        Task<EntityList<TEntity>> GetAll(
                        Expression<Func<TEntity, bool>> filter = null,
                        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                        IEnumerable<Expression<Func<TEntity, object>>> includes = null,
                        int? page = null, int? pageSize = null);

        Task<TEntity> GetById(object id);

        Task<TEntity> Insert(TEntity entityToInsert);

        Task<TEntity> Update(TEntity entitytoUpdate);

        Task<int> Delete(object id);
        Task<int> Delete(TEntity entityToDelete);

    }
}
