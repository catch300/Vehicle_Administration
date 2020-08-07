using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicle.Common
{
    public  class Sorting : ISorting
    {
        public string SortOrder { get; set; }
        public Sorting( ) { }

        public Sorting(string sortOrder ) 
        {
            SortOrder = sortOrder;
        }
    }
}
