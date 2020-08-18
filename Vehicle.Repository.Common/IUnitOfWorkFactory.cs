using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicle.Repository.Common
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create( );
    }
}
