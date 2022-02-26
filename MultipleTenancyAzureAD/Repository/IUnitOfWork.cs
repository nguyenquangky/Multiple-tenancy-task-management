using MultiTenancyAzureAD.Data;

namespace MultiTenancyAzureAD.Main.Repository
{
    public interface IUnitOfWork
    {
        MultiTenantDbContext Context { get; }
        void Save();
    }
}