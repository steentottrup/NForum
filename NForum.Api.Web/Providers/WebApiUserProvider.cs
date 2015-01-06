using NForum.Core;
using NForum.Core.Abstractions.Providers;
using NForum.Core.Abstractions.Services;
using System;
using System.Threading;

namespace NForum.Api.Web.Providers {

	public class WebApiUserProvider : IUserProvider {
		protected readonly IUserService userService;

		public WebApiUserProvider(IUserService userService) {
			this.userService = userService;
		}

		public User CurrentUser {
			get {
				if (this.Authenticated) {
					return this.userService.Read(Thread.CurrentPrincipal.Identity.Name);
				}
				return null;
			}
		}

		public Boolean Authenticated {
			get {
				return Thread.CurrentPrincipal.Identity.IsAuthenticated;
			}
		}
	}
}