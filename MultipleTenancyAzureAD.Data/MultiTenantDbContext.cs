using MultipleTenancyAzureAD.Core;
using MultiTenancyAzureAD.Core;
using System.Data.Entity;

namespace MultiTenancyAzureAD.Data
{
    [DbConfigurationType(typeof(DataConfiguration))]
    public class MultiTenantDbContext : DbContext
    {
        public MultiTenantDbContext() : base(ConnectionString())
        {
        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Task> Tasks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        private static string ConnectionString()
        {
            //Tenant t = new TenantService().Tenant;
            Tenant t = TenantHelper.Tenant;
            if(t == null)
            {
                // fix case of run update-database
                // use default connection string
            }
            return t.ConnectionString;
        }
    }

    public class DataConfiguration: DbConfiguration
    {
        public DataConfiguration()
        {
            SetDatabaseInitializer(new MultiTenantDbContextInitializer());
        }
    }

    public class MultiTenantDbContextInitializer : CreateDatabaseIfNotExists<MultiTenantDbContext>
    {
        public MultiTenantDbContextInitializer()
        {
        }
        protected override void Seed(MultiTenantDbContext context)
        {
            
        }
    }
}