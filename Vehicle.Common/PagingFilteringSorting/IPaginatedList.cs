using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle.Common.PagingFilteringSorting
{
    public interface IPaginatedList<TEntity> where TEntity : class
    {
        public int? Page { get; set; }
        public int TotalPages { get; set; }
        public int? PageSize { get; set; }
        public int CountItems { get; set; }
        public bool HasPreviousPage { get; }
        public bool HasNextPage { get; }
        public IReadOnlyList<TEntity> Data { get; set; }


    }
}
