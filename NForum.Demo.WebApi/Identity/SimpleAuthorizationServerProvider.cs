using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NForum.Demo.WebApi.Identity {

	public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider {

		public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context) {
			context.Validated();
		}

		public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context) {

			context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

			AuthContext _ctx = new AuthContext();
			using (UserManager<IdentityUser> _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_ctx))) {
				IdentityUser user = await _userManager.FindAsync(context.UserName, context.Password);

				if (user == null) {
					context.SetError("invalid_grant", "The user name or password is incorrect.");
					return;
				}
			}

			var identity = new ClaimsIdentity(context.Options.AuthenticationType);
			identity.AddClaim(new Claim("sub", context.UserName));
			identity.AddClaim(new Claim("role", "user"));
			identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));

			context.Validated(identity);

		}
	}
}