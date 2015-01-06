[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NForum.Demo.WebApi.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NForum.Demo.WebApi.App_Start.NinjectWebCommon), "Stop")]

namespace NForum.Demo.WebApi.App_Start {
	using Microsoft.Web.Infrastructure.DynamicModuleHelper;
	using NForum.Core.Abstractions;
	using NForum.Core.Abstractions.Data;
	using NForum.Core.Abstractions.Events;
	using NForum.Core.Abstractions.Providers;
	using NForum.Core.Abstractions.Services;
	using NForum.Core.Events;
	using NForum.Core.Services;
	using NForum.Demo.WebApi.Identity;
	using NForum.Demo.WebApi.Providers;
	using NForum.Logging.NLog;
	using NForum.Persistence.EntityFramework;
	using NForum.Persistence.EntityFramework.Repositories;
	using Ninject;
	using Ninject.Web.Common;
	using System;
	using System.Web;

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
			kernel.Bind<ICategoryService>().To<CategoryService>();
			kernel.Bind<IUserProvider>().To<WebApiUserProvider>();
			kernel.Bind<IEventPublisher>().To<EventPublisher>();
			kernel.Bind<ILogger>().To<NLogLogger>();
			kernel.Bind<IState>().To<WebApiState>();
			kernel.Bind<IPermissionService>().To<PermissionService>();
			kernel.Bind<IForumService>().To<ForumService>();

			kernel.Bind<IUserService>().To<UserService>();
			kernel.Bind<IUserRepository>().To<UserRepository>();

			kernel.Bind<ICategoryRepository>().To<CategoryRepository>();
			kernel.Bind<IForumRepository>().To<ForumRepository>();

			// Entity Framework implementation specific
			kernel.Bind<UnitOfWork>().To<UnitOfWork>();

			// Demo site specific:
			kernel.Bind<AuthRepository>().To<AuthRepository>();
		}
	}
}