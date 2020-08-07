using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Vehicle.Common
{
    public class PaginatedList<T> : List<T>, IPaginatedList<T>
    {
        public int? PageIndex { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<T> Data { get; set; }
        public new int Count { get; set; }
        

        public PaginatedList( ) { }

        public PaginatedList(IEnumerable<T> items, int count, int? pageIndex, int pageSize)
        {
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


        public  async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {

            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
}
