using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Vehicle.Model;
using Vehicle.Model.Common;

namespace Vehicle.WebAPI.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile( )
        {
            CreateMap<IVehicleMakeViewModel, IVehicleMake>().ReverseMap();

            //CreateMap<IVehicleModelViewModel, IVehicleModel>().ReverseMap();


        }
    }
}
