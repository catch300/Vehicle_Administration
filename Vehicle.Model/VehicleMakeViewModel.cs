using System;
using System.Collections.Generic;
using System.Text;
using Vehicle.Model.Common;

namespace Vehicle.Model
{
    public class VehicleMakeViewModel : IVehicleMakeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
    }
}
