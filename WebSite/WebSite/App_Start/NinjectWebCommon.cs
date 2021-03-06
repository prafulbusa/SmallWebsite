[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(WebSite.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(WebSite.App_Start.NinjectWebCommon), "Stop")]

namespace WebSite.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using RepositoriesFacade;
    using WebSite.Context;
    using RepositoriesFacade.Concrete;
    using Repositories.Concrete;
    using ServicesFacade.Concrete;
    using Services.Concrete;
    using Repositories;
    using Interfaces;
    using AdditionalServices;
    using System.Net.Configuration;
    using System.Configuration;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
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

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IContextFactory>().To<ContextFactory>().InRequestScope();
            kernel.Bind<IMessageRepository>().To<MessageRepository>();
            
            kernel.Bind<IMessageService>().To<MessageService>();
            kernel.Bind<IEmailSender>().To<EmailSender>().WithConstructorArgument<SmtpSection>(ConfigurationManager.GetSection("system.net/mailSettings/smtp") as SmtpSection);

            kernel.Bind(typeof(IRepository<>))
                .To(typeof(Repository<>))
                .InRequestScope();
        }        
    }
}
