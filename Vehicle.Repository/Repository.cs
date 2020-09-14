using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Vehicle.Common.PagingFilteringSorting;
using Vehicle.DAL;

using Vehicle.Repository.Common;

namespace Vehicle.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        public VehicleContext _context;
        internal DbSet<TEntity> dbSet;

        public Repository(VehicleContext context)
        {
            _context = context;
            this.dbSet = context.Set<TEntity>();
        }

        //GET ALL ENTITIES
        public async Task<IEnumerable<TEntity>> GetAll( )
        {
            return await GetAll(null, null, null, null, null);
        }

        public async Task<IEnumerable<TEntity>> GetAll(
            Expression<Func<TEntity, bool>> filter = null)
        {
            return await GetAll(filter, null, null, null, null);
        }

        public async Task<IEnumerable<TEntity>> GetAll(
            Expression<Func<TEntity, bool>> filter = null,
            string[] includePaths = null)
        {
            return await GetAll(filter, includePaths, null, null, null);
        }

        public async Task<IEnumerable<TEntity>> GetAll(
           Expression<Func<TEntity, bool>> filter = null,
           string[] includePaths = null,
           int? page = null,
           int? pageSize = null,
           params SortExpression<TEntity>[] sortExpressions)
        {
            IQueryable<TEntity> query = dbSet;
            ;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includePaths != null)
            {
                for (var i = 0; i < includePaths.Count(); i++)
                {
                    query = query.Include(includePaths[i]);
                }
            }

            if (sortExpressions != null)
            {
                IOrderedQueryable<TEntity> orderedQuery = null;
                for (var i = 0; i < sortExpressions.Count(); i++)
                {
                    if (i == 0)
                    {
                        if (sortExpressions[i].SortDirection == ListSortDirection.Ascending)
                        {
                            orderedQuery = query.OrderBy(sortExpressions[i].SortBy);
                        }
                        else
                        {
                            orderedQuery = query.OrderByDescending(sortExpressions[i].SortBy);
                        }
                    }
                    else
                    {
                        if (sortExpressions[i].SortDirection == ListSortDirection.Ascending)
                        {
                            orderedQuery = orderedQuery.ThenBy(sortExpressions[i].SortBy);
                        }
                        else
                        {
                            orderedQuery = orderedQuery.ThenByDescending(sortExpressions[i].SortBy);
                        }

                    }
                }

                if (page != null)
                {
                    query = orderedQuery.Skip(((int)page - 1) * (int)pageSize);
                }
            }

            if (pageSize != null)
            {
                query = query.Take((int)pageSize);
            }

            return await query.ToListAsync();
        }

        //GET ALL ENTITIES


        //public async Task<IPaginatedList<TEntity>> GetAll(
        //        Expression<Func<TEntity, bool>> filter = null,
        //        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        //        IEnumerable<Expression<Func<TEntity, object>>> includes = null,
        //        int? page = null, int? pageSize = null)
        //{

        //    var query = dbSet.AsQueryable();
        //    if (includes != null)
        //    {
        //        query = includes.Aggregate(query, (current, include) => current.Include(include));
        //    }

        //    int totalCount = query.Count();
        //    int filteredCount = totalCount;

        //    if (filter != null)
        //    {
        //        query = query.Where(filter);
        //        filteredCount = query.Count();
        //    }

        //    if (orderBy != null)
        //    {
        //        query = orderBy(query);
        //    }
        //    if (page != null && pageSize != null)
        //    {
        //        query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
        //        ;
        //    }

        //    var pageData = await query.ToListAsync();

        //    IPaginatedList<TEntity> listOfEntities = new EntityList<TEntity>
        //    {
        //        TotalCount = totalCount,
        //        FilteredCount = filteredCount,
        //        PageData = pageData,
        //    };

        //    return listOfEntities;
        //}

        //GET ENTITY BY ID
        public async Task<TEntity> GetById(object id)
        {
            return await dbSet.FindAsync(id);
        }

        //ADD ENTITY
        public async Task<int> Add(TEntity entityToInsert)
        {
            EntityEntry dbEntityEntry = _context.Entry(entityToInsert);
            if (dbEntityEntry.State != EntityState.Detached)
            {
                dbEntityEntry.State = EntityState.Added;
            }
            else
            {
                await dbSet.AddAsync(entityToInsert);
            }
            return await Task.FromResult(1);

        }

        //UPDATE ENTITY
        public async Task<int> Update(TEntity entityToUpdate)
        {

            EntityEntry dbEntityEntry = _context.Entry(entityToUpdate);
            if (dbEntityEntry.State == EntityState.Detached)
            {
                dbSet.Attach(entityToUpdate);
            }
            dbEntityEntry.State = EntityState.Modified;

            return await Task.FromResult(1);

        }

        //DEKETE ENTITY BY ID
        public async Task<int> Delete(object id)
        {
            var entity = dbSet.Find(id);
            if (entity == null)
            {
                return await Task.FromResult(0);
            }
            return await Delete(entity);
        }

        //DELETE ENTITY
        public async Task<int> Delete(TEntity entityToDelete)
        {
            EntityEntry dbEntityEntry = _context.Entry(entityToDelete);
            if (dbEntityEntry.State != EntityState.Deleted)
            {
                dbEntityEntry.State = EntityState.Deleted;
            }
            else
            {
                dbSet.Attach(entityToDelete);
                dbSet.Remove(entityToDelete);
            }
            return await Task.FromResult(1);
        }

        private enum OrderByType
        {

            Ascending,
            Descending
        }
        public void Dispose( )
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
            }
        }
    }
}
