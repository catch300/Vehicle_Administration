using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Vehicle.Common.PagingFilteringSorting;

namespace Vehicle.Repository.Common
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IPaginatedList<TEntity>> GetAll(
                         Expression<Func<TEntity, bool>> filter = null,
                         Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                         IEnumerable<Expression<Func<TEntity, object>>> includes = null,
                         int? page = null, int? pageSize = null);

        //Task<IEnumerable<TEntity>> GetAll( );

        //Task<IEnumerable<TEntity>> GetAll(
        //   Expression<Func<TEntity, bool>> filter = null);

        //Task<IEnumerable<TEntity>> GetAll(
        //   Expression<Func<TEntity, bool>> filter = null,
        //   string[] includePaths = null);

        //Task<IEnumerable<TEntity>> GetAll(
        //   Expression<Func<TEntity, bool>> filter = null,
        //   string[] includePaths = null,
        //   int? page = 0,
        //   int? pageSize = null,
        //   params SortExpression<TEntity>[] sortExpressions);

        Task<TEntity> GetById(object id);

        Task<int> Add(TEntity entityToInsert);

        Task<int> Update(TEntity entityToUpdate);

        Task<int> Delete(object id);
        Task<int> Delete(TEntity entityToDelete);

    }
}
