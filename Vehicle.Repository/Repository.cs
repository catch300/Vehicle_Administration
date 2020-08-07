using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Vehicle.Common;
using Vehicle.Common.PagingFilteringSorting;
using Vehicle.DAL;
using Vehicle.Model;
using Vehicle.Model.Common;
using Vehicle.Repository.Common;

namespace Vehicle.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly VehicleContext _context;
        internal DbSet<TEntity> dbSet;

        public Repository( VehicleContext context)
        {
            _context = context;
            dbSet = context.Set<TEntity>();
        }

       

        //GET ALL VEHICLES
        public async Task<EntityList<TEntity>> GetAll(
    Expression<Func<TEntity, bool>> filter = null,
    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
    IEnumerable<Expression<Func<TEntity, object>>> includes = null,
    int? page = null, int? pageSize = null)
        {
            var query = _context.Set<TEntity>().AsQueryable();
            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            int totalCount = query.Count();
            int filteredCount = totalCount;

            if (filter != null)
            {
                query = query.Where(filter);
                filteredCount = query.Count();
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (page != null && pageSize != null)
            {
                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }

            var pageData = await query.ToListAsync();

            return new EntityList<TEntity>
            {
                TotalCount = totalCount,
                FilteredCount = filteredCount,
                PageData = pageData,
            };
        }

        //GET VEHICLE BY ID
        public async Task<TEntity> GetById(object id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<TEntity> Insert(TEntity entityToInsert)
        {
           await dbSet.AddAsync(entityToInsert);
           await _context.SaveChangesAsync();
           return entityToInsert;
        }

        public async Task<TEntity> Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entityToUpdate;
        }

        public async Task<int> Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            await Delete(entityToDelete);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(TEntity entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
            return await _context.SaveChangesAsync();
        }
    }
}
