using NForum.Web;
using Ninject;
using Ninject.Activation;
using Ninject.Infrastructure.Disposal;
using System;
using System.Threading;

namespace NForum.DependencyInjection.Ninject {

	public static class INForumBuilderExtensions {
		private static readonly AsyncLocal<Scope> scopeProvider = new AsyncLocal<Scope>();

		private static IReadOnlyKernel kernel;
		private static Object Resolve(Type type) => kernel.Get(type);
		private static Scope RequestScope(IContext context) => scopeProvider.Value;

		private sealed class Scope : DisposableObject { }

		public static INForumApplicationBuilder WithNinject(this INForumApplicationBuilder app) {
			IKernelConfiguration config = new KernelConfiguration();

			// Register application services
			config.Bind(app.ApplicationBuilder.GetControllerTypes()).ToSelf().InScope(RequestScope);

			config.Bind<IUserService>().To<AspNetUserService>().InScope(RequestScope);
			//config.Bind<CustomMiddleware>().ToSelf();

			// Cross-wire required framework services
			config.BindToMethod(app.GetRequestService<IViewBufferScope>);
			//config.Bind<ILoggerFactory>().ToConstant(loggerFactory);

			config.Bind<ICommandDispatcher>().To<CommandDispatcher>();
			config.Bind<IQueryDispatcher>().To<QueryDispatcher>();

			config.Bind<IHttpContextAccessor>().ToMethod((context) => {
				return app.ApplicationServices.GetRequiredService<IHttpContextAccessor>();
			}).InScope(RequestScope);

			config.Bind<IPrincipal>().ToMethod((context) => {
				return context.Kernel.Get<IHttpContextAccessor>().HttpContext.User;
			}).InScope(RequestScope);

			config
				.Bind(typeof(IGenericValidationCommandHandlerDecorator<>))
				.To(typeof(GenericValidationCommandHandlerDecorator<>));
			config
				.Bind(typeof(IGenericPermissionCheckCommandHandlerDecorator<>))
				.To(typeof(GenericPermissionCheckCommandHandlerDecorator<>));

			config
				.Bind(typeof(IGenericValidationQueryHandlerDecorator<,>))
				.To(typeof(GenericValidationQueryHandlerDecorator<,>));
			config
				.Bind(typeof(IGenericPermissionCheckQueryHandlerDecorator<,>))
				.To(typeof(GenericPermissionCheckQueryHandlerDecorator<,>));

			Assembly coreAssembly = typeof(NForum.CQS.CommandWithStatus).GetTypeInfo().Assembly;

			config.BindQueryHandlers(coreAssembly);

			config.BindCommandHandlers(coreAssembly);

			config.BindPermissionChecks(coreAssembly);

			config.BindValidators(coreAssembly);

			kernel = config.BuildReadonlyKernel();

			return builder;
		}
	}

	public class AspNetUserService : IUserService { }
}
