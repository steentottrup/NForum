using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using NForum.Demo.WebApi.Identity;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

[assembly: OwinStartup(typeof(NForum.Demo.WebApi.Startup))]
namespace NForum.Demo.WebApi {

	public class Startup {

		public void Configuration(IAppBuilder app) {
			ConfigureOAuth(app);

			HttpConfiguration config = new HttpConfiguration();
			GlobalConfiguration.Configure(NForum.Api.Web.RouteHack.Register);

			app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
//			app.UseWebApi(config);
		}

		public void ConfigureOAuth(IAppBuilder app) {
			OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions() {
				AllowInsecureHttp = true,
				TokenEndpointPath = new PathString("/token"),
				AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
				Provider = new SimpleAuthorizationServerProvider()
			};

			// Token Generation
			app.UseOAuthAuthorizationServer(OAuthServerOptions);
			app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

		}
	}
}