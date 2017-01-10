using Microsoft.Extensions.DependencyInjection;
using System;

namespace NForum.Builder {

	public static class ServiceCollectionExtensions {

		public static INForumBuilder AddNForum(this IMvcBuilder services) {
			// TODO:

			return new NForumBuilder { Services = services.Services, PartManager = services.PartManager };
		}
	}
}
