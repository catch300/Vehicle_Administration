using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vehicle.DAL;
using Vehicle.Model;

using AutoMapper;
using Vehicle.Model.Common;
using Vehicle.Repository;
using Vehicle.Repository.Common;
using Vehicle.WebAPI.ViewModels;
using Vehicle.Common.PagingFilteringSorting;
using Vehicle.Service.Common;

namespace Vehicle.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleMakeController : ControllerBase
    {
        private readonly IMapper _mapper;

        private readonly IUnitOfWork _unitOfWork;


        public VehicleMakeController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        // GET: api/VehicleMake
        [HttpGet]
        public async Task<IEnumerable<VehicleMake>> GetVehicleMakes()
        {
            //using var unitOfWork = _unitOfWorkFactory.Create();

            //var listOfvehicleMakes = await unitOfWork.Repository<VehicleMakeVM>().GetAll();
            var vehicleMakes = await _unitOfWork.Repository<VehicleMake>().GetAll();
            //var mapper = _mapper.Map<IEnumerable<VehicleMakeVM>>(vehicleMakes);


            return vehicleMakes;
        }

        // GET: api/VehicleMake/5
        [HttpGet("{id}")]
        public async Task<IVehicleMake> GetVehicleMake(int id)
        {
            //using IUnitOfWork unitOfWork = _unitOfWorkFactory.Create();
            var vehicleMakeID = await _unitOfWork.Repository<VehicleMake>().GetById(id);
            var mapper = _mapper.Map<VehicleMakeVM>(vehicleMakeID);

            
            if (mapper == null)
            {
                throw new ArgumentNullException("vehicleMakeID");
            }

            return vehicleMakeID;
        }

        // PUT: api/VehicleMake/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicleMake(int id, VehicleMake vehicleMake)
        {
            //using IUnitOfWork unitOfWork = _unitOfWorkFactory.Create();
            if (id != vehicleMake.Id)
            {
                return BadRequest();
            }
            
            await _unitOfWork.Repository<VehicleMake>().Update(vehicleMake );
           
            try
            {
               await _unitOfWork.CommitAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        // POST: api/VehicleMake
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<IVehicleMake>> PostVehicleMake(VehicleMake vehicleMake)
        {
            //using IUnitOfWork unitOfWork = _unitOfWorkFactory.Create();
            await _unitOfWork.Repository<VehicleMake>().Add(vehicleMake);
            await _unitOfWork.CommitAsync();

            return CreatedAtAction("GetVehicleMake", new { id = vehicleMake.Id }, vehicleMake);
        }

        // DELETE: api/VehicleMake/5
        [HttpDelete("{id}")]
        public async Task<IVehicleMake> DeleteVehicleMake(int id)
        {
            //using IUnitOfWork unitOfWork = _unitOfWorkFactory.Create();


            var vehicleMake = await _unitOfWork.Repository<VehicleMake>().GetById(id);

            if (vehicleMake == null)
            {
                throw new ArgumentNullException("IVehicleMake");
            }

            await _unitOfWork.Repository<VehicleMake>().Delete(vehicleMake);
            await _unitOfWork.CommitAsync();

            return vehicleMake;
        }
        


    }
}
