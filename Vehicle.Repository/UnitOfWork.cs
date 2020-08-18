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
        public Dictionary<Type, object> repositories = new Dictionary<Type, object>();


        public UnitOfWork(VehicleContext context )
        {
            _context = context;
        }

        
        //VehicleMakeRepository
        public IRepository<T> VehicleMakeRepository<T>() where T : class
        {

            if (repositories.Keys.Contains(typeof(T)) == true)
            {
                return repositories[typeof(T)] as IRepository<T>;
            }
            IRepository<T> repo = new Repository<T>(_context);
            repositories.Add(typeof(T), repo);
            return repo;
            //get
            //{
            //    if (this.vehicleMakeRepository == null)
            //    {
            //        this.vehicleMakeRepository = new Repository<VehicleMake>(_context);
            //    }
            //    return vehicleMakeRepository;
            //}

        }

       



        //VehicleModelRepository


        public async Task<int> CommitAsync()
        {
            int result = 0;
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                result = await _context.SaveChangesAsync();
                scope.Complete();
            }
            return result;
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
