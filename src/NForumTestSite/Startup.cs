using CreativeMinds.CQS.Commands;
using CreativeMinds.CQS.Ninject;
using CreativeMinds.CQS.Permissions;
using CreativeMinds.CQS.Queries;
using CreativeMinds.CQS.Validators;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using NForum.Datastores;
using NForum.Infrastructure;
using Ninject;
using Ninject.Activation;
using Ninject.Infrastructure.Disposal;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection;
using System.Security.Principal;
using System.Threading;

namespace NForumTestSite {

	public class Startup {
		private readonly AsyncLocal<Scope> scopeProvider = new AsyncLocal<Scope>();

		private IReadOnlyKernel kernel;
		private Object Resolve(Type type) => kernel.Get(type);
		private Scope RequestScope(IContext context) => scopeProvider.Value;

		public Startup(IHostingEnvironment env) {
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
				.AddEnvironmentVariables();
			Configuration = builder.Build();
		}

		public IConfigurationRoot Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		//public IServiceProvider ConigreServices(IServiceCollection services) { 
		public void ConfigureServices(IServiceCollection services) {
			// Add framework services.
			services
				.AddMvc();
			//.AddNForum()
			//.AddNinject();

			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			services.AddRequestScopingMiddleware(() => scopeProvider.Value = new Scope());
			services.AddCustomControllerActivation(Resolve);
			services.AddCustomViewComponentActivation(Resolve);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory) {
			loggerFactory.AddConsole(Configuration.GetSection("Logging"));
			loggerFactory.AddDebug();

			kernel = RegisterApplicationComponents(app, loggerFactory);

			//app.Use(async (context, next) => {
			//	await kernel.Get<CustomMiddleware>().Invoke(context, next);
			//});

			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
				app.UseBrowserLink();
			}
			else {
				app.UseExceptionHandler("/Home/Error");
			}

			app.UseStaticFiles();

			app.UseMvc(routes => {
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});
		}

		private IMongoDatabase db;

		private IReadOnlyKernel RegisterApplicationComponents(IApplicationBuilder app, ILoggerFactory loggerFactory) {
			IKernelConfiguration config = new KernelConfiguration();

			// Register application services
			config.Bind(app.GetControllerTypes()).ToSelf().InScope(RequestScope);

			config.Bind<IUserService>().To<AspNetUserService>().InScope(RequestScope);
			//config.Bind<CustomMiddleware>().ToSelf();

			// Cross-wire required framework services
			config.BindToMethod(app.GetRequestService<IViewBufferScope>);
			config.Bind<ILoggerFactory>().ToConstant(loggerFactory);




			// TODO: Handle this in the Ninject NForum builder!!!
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

			// TODO: Dapper wire up!
			//config
			//	.Bind<IDbConnection>()
			//	.ToMethod((context) => {
			//		IDbConnection conn =  new SqlConnection("Server=.\\SQLEXPRESS2008R2; Database=nforum-dapper;User id=sa;Password=ivypqn63;");
			//		conn.Open();

			//		return conn;
			//	}).InScope(RequestScope);

			//config
			//	.Bind<ICategoryDatastore>()
			//	.To<NForum.Datastores.Dapper.CategoryDatastore>();
			// TODO: End Dapper

			// TODO: MongoDB wire up!
			config
				.Bind<ICategoryDatastore>()
				.To<NForum.Datastores.MongoDB.CategoryDatastore>();

			MongoUrl url = new MongoUrl("mongodb://127.0.0.1/nforum");
			db = new MongoClient(url).GetDatabase(url.DatabaseName);

			config
				.Bind<IMongoCollection<NForum.Datastores.MongoDB.Dbos.Category>>()
				.ToMethod((context) => {
					return db.GetCollection<NForum.Datastores.MongoDB.Dbos.Category>("categories");
				});
			config
				.Bind<IMongoCollection<NForum.Datastores.MongoDB.Dbos.Forum>>()
				.ToMethod((context) => {
					return db.GetCollection<NForum.Datastores.MongoDB.Dbos.Forum>("forums");
				});
			config
				.Bind<IMongoCollection<NForum.Datastores.MongoDB.Dbos.Topic>>()
				.ToMethod((context) => {
					return db.GetCollection<NForum.Datastores.MongoDB.Dbos.Topic>("topics");
				});
			config
				.Bind<IMongoCollection<NForum.Datastores.MongoDB.Dbos.Reply>>()
				.ToMethod((context) => {
					return db.GetCollection<NForum.Datastores.MongoDB.Dbos.Reply>("replies");
				});
			config
				.Bind<NForum.Datastores.MongoDB.CommonDatastore>()
				.ToSelf();
			// TODO: END MONGO



			config
				.Bind<IBoardConfiguration>()
				.To<BuilderBoardConfiguration>();

			return config.BuildReadonlyKernel();
		}

		private sealed class Scope : DisposableObject { }
	}

	public static class BindingHelpers {
		public static void BindToMethod<T>(this IKernelConfiguration config, Func<T> method) => config.Bind<T>().ToMethod(c => method());
	}

	public interface IUserService { }

	public class AspNetUserService : IUserService { }
}
