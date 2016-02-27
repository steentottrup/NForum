using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace NForum.Web {

	public class RouteConfig {
		public static void RegisterRoutes(RouteCollection routes) {
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			// TODO: For now!!
			routes.IgnoreRoute("index.html");

			routes.MapMvcAttributeRoutes();

			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
				namespaces: new String[] { typeof(NForum.Web.Controllers.HomeController).Namespace }
			);
		}
	}
}
