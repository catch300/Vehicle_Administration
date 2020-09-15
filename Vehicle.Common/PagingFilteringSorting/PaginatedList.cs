using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Vehicle.Common.PagingFilteringSorting
{
    public class PaginatedList<TEntity> : IPaginatedList<TEntity> where TEntity : class
    {
        public int? Page { get; set; }
        public int TotalPages { get; set; }
        public int? PageSize { get; set; }
        public int CountItems { get; set; }
        public bool HasPreviousPage { get { return Page > 1; } }
        public bool HasNextPage { get { return Page < TotalPages; } }
        public IReadOnlyList<TEntity> Data { get; set; }
    }
}
