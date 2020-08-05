using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicle.Model.Common
{
    public interface IVehicleMake
    {
        int Id { get; set; }
        string Name { get; set; }
        string Abrv { get; set; }
    }
}
