using MultiTenancyAzureAD.Data;
using System;

namespace MultiTenancyAzureAD.Main.Repository
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly MultiTenantDbContext _context;

        public UnitOfWork(MultiTenantDbContext context)
        {
            this._context = context;
        }

        public void Save()
        {
            _context.SaveChanges();
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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public MultiTenantDbContext Context
        {
            get { return _context; }
        }
    }
}