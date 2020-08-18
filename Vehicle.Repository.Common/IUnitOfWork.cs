using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vehicle.Model;
using Vehicle.Repository.Common;

namespace Vehicle.Repository.Common
{
    public interface IUnitOfWork 
    {
        IRepository<T> VehicleMakeRepository<T>( ) where T : class;
        Task<int> CommitAsync();
        
    }
}
