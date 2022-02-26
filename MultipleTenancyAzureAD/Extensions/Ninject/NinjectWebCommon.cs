using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using MultiTenancyAzureAD.Core;
using MultiTenancyAzureAD.Data;
using MultiTenancyAzureAD.Main.Repository;
using MultiTenancyAzureAD.Main.Services;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.Common.WebHost;
using System;
using System.Web;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(MultiTenancyAzureAD.Main.Extensions.Ninject.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(MultiTenancyAzureAD.Main.Extensions.Ninject.NinjectWebCommon), "Stop")]
namespace MultiTenancyAzureAD.Main.Extensions.Ninject
{
    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            Bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            Bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InSingletonScope();

            // register repos
            kernel.Bind<IGenericRepository<Department>>().To<GenericRepository<Department>>().InRequestScope();
            kernel.Bind<IGenericRepository<Task>>().To<GenericRepository<Task>>().InRequestScope();
            // register services
            kernel.Bind<IDepartmentService>().To<DepartmentService>().InRequestScope();
            kernel.Bind<ITaskService>().To<TaskService>().InRequestScope();
        }
    }
}