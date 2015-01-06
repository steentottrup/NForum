using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace NForum.Demo.WebApi.Identity {

	public class AuthContext : IdentityDbContext<IdentityUser> {
		public AuthContext()
			: base("AuthContext") {

		}
	}
}