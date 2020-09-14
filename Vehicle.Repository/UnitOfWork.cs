using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Vehicle.DAL;
using Vehicle.Model;
using Vehicle.Repository.Common;

namespace Vehicle.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly VehicleContext _context;
        public Dictionary<string, object> repositories;


        public UnitOfWork(VehicleContext context)
        {
            _context = context;
        }

        
        //VehicleMakeRepository
        public IRepository<T> Repository<T>( ) where T : class
        {
            if (repositories == null)
            {
                repositories = new Dictionary<string, object>();
            }
            var type = typeof(T).Name;
            if(!repositories.ContainsKey(type))
            {
                var repositoryType = typeof(Repository<>);
                var repositoryInstance =
                    Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context);
                repositories.Add(type, repositoryInstance);
            }

            return (Repository<T>)repositories[type];
        }
     

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose( )
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


    }
}
