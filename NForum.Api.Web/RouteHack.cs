using System;
using System.Web.Http;

namespace NForum.Api.Web {

	public static class RouteHack {

		public static void Register(HttpConfiguration config) {
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);
		}
	}
}