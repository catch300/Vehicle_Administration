using System;
using System.Collections.Generic;
using System.Text;
using Vehicle.DAL;
using Vehicle.Repository.Common;

namespace Vehicle.Repository
{
   public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        public IUnitOfWork Create( )
        {
            return new UnitOfWork(new VehicleContext());
        }
    }
}
