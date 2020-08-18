﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicle.Common.PagingFilteringSorting
{
    public interface IEntityList<TEntity> where TEntity : class
    {
        public int TotalCount { get; set; }
        public int FilteredCount { get; set; }
        public IReadOnlyList<TEntity> PageData { get; set; }
    }
}
