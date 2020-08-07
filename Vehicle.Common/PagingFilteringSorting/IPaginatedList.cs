using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle.Common
{
    public interface IPaginatedList<T> 
    {
        public int? PageIndex { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<T> Data { get; set; }
        public int Count { get; set; }
       

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

        public  Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize);
        
    }
}
