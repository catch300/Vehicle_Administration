using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vehicle.Model;
using Vehicle.Model.Common;

using Vehicle.Common.PagingFilteringSorting;

namespace Vehicle.Service.Common
{
    public interface IVehicleMakeService
    {
        Task<IPaginatedList<IVehicleMake>> GetVehicleMakes( );
        Task<IVehicleMake> GetById(object id);
        Task<int> Add(IVehicleMake entityToInsert);

        Task<int> Update(IVehicleMake entityToUpdate);

        Task<int> Delete(int id);

        Task<int> Delete(IVehicleMake entityToDelete);
    }
}
