using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Vehicle.DAL.Entities
{
    public class VehicleEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }

        //public List<IVehicleModel> VehicleModels { get; set; }

    }
}
