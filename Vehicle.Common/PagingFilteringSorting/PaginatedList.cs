using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Vehicle.Common.PagingFilteringSorting
{
    public class PaginatedList<TEntity> : List<TEntity> /*IPaginatedList<TEntity> where TEntity : class*/
    {
        public int? PageIndex { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<TEntity> Data { get; set; }
        public new int Count { get; set; }

        public PaginatedList( ) { }

        public PaginatedList(IEnumerable<TEntity> items, int count, int? pageIndex, int pageSize)
        {
            if (items == null)
            {
                throw new ArgumentNullException("items");
            }

            Data = items;
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(items);
        }
        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }


        public async Task<PaginatedList<TEntity>> CreateAsync(IQueryable<TEntity> source, int pageIndex, int pageSize)
        {

            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<TEntity>(items, count, pageIndex, pageSize);
        }
    }
}
