using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Vehicle.Model.Common;

namespace Vehicle.Model
{
    [Table("VehicleMake")]
    public class VehicleMake : IVehicleMake
    {
        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} is required field!")]
        [Column(TypeName = "nvarchar(250)")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} is required field!")]
        [Column(TypeName = "nvarchar(250)")]
        [Display(Name = "Abreveation")]
        public string Abrv { get; set; }

        //public List<IVehicleModel> VehicleModels { get; set; }
    }
}
