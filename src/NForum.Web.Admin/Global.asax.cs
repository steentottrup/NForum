using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NForum.Web.Admin {
	public class Global : HttpApplication {

		protected void Application_Start(object sender, EventArgs e) {
			//AreaRegistration.RegisterAllAreas();
			//GlobalConfiguration.Configure(WebApiConfig.Register);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
		}

		protected void Session_Start(object sender, EventArgs e) {

		}

		protected void Application_BeginRequest(object sender, EventArgs e) {

		}

		protected void Application_AuthenticateRequest(object sender, EventArgs e) {

		}

		protected void Application_Error(object sender, EventArgs e) {

		}

		protected void Session_End(object sender, EventArgs e) {

		}

		protected void Application_End(object sender, EventArgs e) {

		}
	}
}