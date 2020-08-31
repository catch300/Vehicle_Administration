
using Microsoft.EntityFrameworkCore;
using Vehicle.Model;



namespace Vehicle.DAL
{
    public class VehicleContext : DbContext
    {
        
        public VehicleContext(DbContextOptions<VehicleContext> options) : base(options) { }
        public VehicleContext( ) : base()
        {

        }

        public DbSet<VehicleMake> VehicleMakes { get; set; }
        //public DbSet<VehicleModel> VehicleModels { get; set; }

        
    }
}
