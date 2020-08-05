using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using AutoMapper;
using Autofac;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using Vehicle.DAL;
using Microsoft.EntityFrameworkCore;
using Vehicle.Model;
using Vehicle.Model.Common;

namespace Vehicle.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<VehicleContext>(options =>
           options.UseSqlServer(Configuration.GetConnectionString("DBConnection")));

        }

        //Container Builder for Autofac 
        public void ConfigureContainer(ContainerBuilder builder)
        {

            builder.RegisterType<VehicleMake>().As<IVehicleMake>();

            //builder.RegisterType<VehicleMakeService>().As<IVehicleMakeService>();
            //builder.RegisterType<VehicleModelService>().As<IVehicleModelService>();
            //builder.RegisterType<Filtering>().As<IFiltering>();
            //builder.RegisterType<Sorting>().As<ISorting>();
            //builder.RegisterType<PaginatedList<VehicleMakeVM>>().As<IPaginatedList<VehicleMakeVM>>();
            //builder.RegisterType<PaginatedList<VehicleModelVM>>().As<IPaginatedList<VehicleModelVM>>();

            builder.AddAutoMapper(typeof(Startup).Assembly);
        }

            // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
            public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
