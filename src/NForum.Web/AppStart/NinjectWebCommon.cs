using System;
using System.Web;

using Microsoft.Web.Infrastructure.DynamicModuleHelper;

using Ninject;
using Ninject.Web.Common;
using NForum.Core.Abstractions.Data;
using NForum.Database.EntityFramework;
using NForum.Core.Abstractions.Services;
using NForum.Core.Services;
using NForum.Core.Abstractions.Providers;
using NForum.Core.Abstractions.Events;
using NForum.Web.Providers;
using NForum.Core.Events;
using NForum.Core.Abstractions;
using NForum.Core.Logging;
using NForum.Database.EntityFramework.Repositories;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NForum.Web.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NForum.Web.NinjectWebCommon), "Stop")]

namespace NForum.Web {
	public static class NinjectWebCommon {
		private static readonly Bootstrapper bootstrapper = new Bootstrapper();

		/// <summary>
		/// Starts the application
		/// </summary>
		public static void Start() {
			DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
			DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
			bootstrapper.Initialize(CreateKernel);
		}

		/// <summary>
		/// Stops the application.
		/// </summary>
		public static void Stop() {
			bootstrapper.ShutDown();
		}

		/// <summary>
		/// Creates the kernel that will manage your application.
		/// </summary>
		/// <returns>The created kernel.</returns>
		private static IKernel CreateKernel() {
			var kernel = new StandardKernel();
			try {
				kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
				kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

				RegisterServices(kernel);
				return kernel;
			}
			catch {
				kernel.Dispose();
				throw;
			}
		}

		/// <summary>
		/// Load your modules or register your services here!
		/// </summary>
		/// <param name="kernel">The kernel.</param>
		private static void RegisterServices(IKernel kernel) {
			/* DATABASE */
			kernel
				.Bind<IDataStore>()
				.To<EntityFrameworkDataStore>()
				.InRequestScope();
			kernel
				.Bind(typeof(IRepository<>))
				.To(typeof(GenericRepository<>));

			/* SERVICES */
			kernel
				.Bind<IForumService>()
				.To<ForumService>();
			kernel
				.Bind<ICategoryService>()
				.To<CategoryService>();
			kernel
				.Bind<IPermissionService>()
				.To<PermissionService>();
			kernel
				.Bind<ILoggingService>()
				.To<LoggingService>();

			/* PROVIDERS and more! */
			kernel
				.Bind<IUserProvider>()
				.To<MvcUserProvider>()
				.InRequestScope();
			kernel
				.Bind<IEventPublisher>()
				.To<EventPublisher>();
			// TODO:
			kernel
				.Bind<ICoreLogger>()
				.To<NullLogger>();
			// TODO:
			kernel
				.Bind<IApplicationLogger>()
				.To<NullLogger>();
		}
	}
}
