using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Vehicle.Common.PagingFilteringSorting;
using Vehicle.Model;
using Vehicle.Model.Common;
using Vehicle.WebAPI.ViewModels;

namespace Vehicle.WebAPI.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile( )
        {
            CreateMap<VehicleMakeVM, VehicleMake > ().ReverseMap();
            

            //CreateMap<IVehicleModelViewModel, IVehicleModel>().ReverseMap();


        }
    }
}
