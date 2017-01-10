using NForum.Builder;
using System;

namespace NForum.DependencyInjection.Ninject {

	public static class INForumBuilderExtensions {

		public static INForumBuilder AddNinject(this INForumBuilder builder) {
			// TODO:
			//builder.Services.AddRequestScopingMiddleware(() => scopeProvider.Value = new Scope());
			//builder.Services.AddCustomControllerActivation(Resolve);
			//builder.Services.AddCustomViewComponentActivation(Resolve);

			return new NForumNinjectBuilder { Services = builder.Services, PartManager = builder.PartManager };
		}
	}
}
