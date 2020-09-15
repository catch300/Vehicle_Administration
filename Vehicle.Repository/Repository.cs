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
        public async Task<IPaginatedList<TEntity>> GetAll(
                Expression<Func<TEntity, bool>> filter = null,
                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                IEnumerable<Expression<Func<TEntity, object>>> includes = null,
                int? page = null, int? pageSize = null)
        {

            var query = dbSet.AsQueryable();
            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }
            
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }
           
            if (page != null && pageSize != null)
            {
                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }

            var data = await query.ToListAsync();
            int count = await query.CountAsync();

            IPaginatedList<TEntity> listOfEntities = new PaginatedList<TEntity>
            {
                Page = page,
                PageSize = pageSize,
                CountItems = count,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize),
                Data = data
            };

            return listOfEntities;
        }

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
