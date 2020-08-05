using System;
using Microsoft.EntityFrameworkCore;
using Vehicle.Model;

namespace Vehicle.DAL
{
    public class VehicleContext : DbContext
    {
       
        public VehicleContext( )
        {
        }

        public VehicleContext(DbContextOptions<VehicleContext> options) : base(options)
        {

        }
        public DbSet<VehicleMake> VehicleMakes { get; set; }
        //public DbSet<VehicleModel> VehicleModels { get; set; }
    }
}
