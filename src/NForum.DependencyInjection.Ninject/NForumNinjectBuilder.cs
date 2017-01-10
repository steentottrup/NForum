using Microsoft.Extensions.DependencyInjection;
using NForum.Builder;
using System;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Ninject;
using System.Threading;
using Ninject.Activation;

namespace NForum.DependencyInjection.Ninject {

	public class NForumNinjectBuilder : INForumBuilder, IMvcBuilder {
		//private readonly AsyncLocal<Scope> scopeProvider = new AsyncLocal<Scope>();
		//private IReadOnlyKernel kernel;
		//private Object Resolve(Type type) => kernel.Get(type);
		//private Scope RequestScope(IContext context) => scopeProvider.Value;

		public ApplicationPartManager PartManager { get; set; }
		public IServiceCollection Services { get; set; }
	}
}
