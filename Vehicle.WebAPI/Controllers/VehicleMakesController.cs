using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vehicle.DAL;
using Vehicle.Model;

namespace Vehicle.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleMakesController : ControllerBase
    {
        private readonly VehicleContext _context;

        public VehicleMakesController(VehicleContext context)
        {
            _context = context;
        }

        // GET: api/VehicleMakes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleMake>>> GetVehicleMakes()
        {
            return await _context.VehicleMakes.ToListAsync();
        }

        // GET: api/VehicleMakes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleMake>> GetVehicleMake(int id)
        {
            var vehicleMake = await _context.VehicleMakes.FindAsync(id);

            if (vehicleMake == null)
            {
                return NotFound();
            }

            return vehicleMake;
        }

        // PUT: api/VehicleMakes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicleMake(int id, VehicleMake vehicleMake)
        {
            if (id != vehicleMake.Id)
            {
                return BadRequest();
            }

            _context.Entry(vehicleMake).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleMakeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/VehicleMakes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<VehicleMake>> PostVehicleMake(VehicleMake vehicleMake)
        {
            _context.VehicleMakes.Add(vehicleMake);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVehicleMake", new { id = vehicleMake.Id }, vehicleMake);
        }

        // DELETE: api/VehicleMakes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<VehicleMake>> DeleteVehicleMake(int id)
        {
            var vehicleMake = await _context.VehicleMakes.FindAsync(id);
            if (vehicleMake == null)
            {
                return NotFound();
            }

            _context.VehicleMakes.Remove(vehicleMake);
            await _context.SaveChangesAsync();

            return vehicleMake;
        }

        private bool VehicleMakeExists(int id)
        {
            return _context.VehicleMakes.Any(e => e.Id == id);
        }
    }
}
