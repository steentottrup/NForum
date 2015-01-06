using System;
using System.Web.Http;
using System.Web.Routing;

namespace NForum.Demo.WebApi {
	
	public class WebApiApplication : System.Web.HttpApplication {

		protected void Application_Start() {
			//GlobalConfiguration.Configure(NForum.Api.Web.RouteHack.Register);
		}
	}
}
