using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vehicle.Common.PagingFilteringSorting;
using Vehicle.Model;
using Vehicle.Model.Common;
using Vehicle.Repository.Common;
using Vehicle.Service.Common;

namespace Vehicle.Service
{
    public class VehicleMakeService : IVehicleMakeService
    {
        private readonly IRepository<IVehicleMake> _vehicleMakeRepository;

        public VehicleMakeService( )
        {
                    
        }
        public VehicleMakeService(IRepository<IVehicleMake> vehicleMakeRepository)
        {
            _vehicleMakeRepository = vehicleMakeRepository;
        }

        //GET ALL
        public async Task<IPaginatedList<IVehicleMake>> GetVehicleMakes( )
        {
            return await  _vehicleMakeRepository.GetAll();
        }

        //GET BY ID
        public async Task<IVehicleMake> GetById(object id)
        {
            return await _vehicleMakeRepository.GetById(id);
        }

        //ADD VehicleMake

        public async Task<int> Add(IVehicleMake entityToInsert)
        {
            return await _vehicleMakeRepository.Add(entityToInsert);
        }

        public async Task<int> Update(IVehicleMake entityToUpdate)
        {
            return await _vehicleMakeRepository.Update(entityToUpdate);
        }

        public async Task<int> Delete(int id)
        {
            return await _vehicleMakeRepository.Delete(id);
        }

        public async Task<int> Delete(IVehicleMake entityToDelete)
        {
            return await _vehicleMakeRepository.Delete(entityToDelete);
        }
    }
}
