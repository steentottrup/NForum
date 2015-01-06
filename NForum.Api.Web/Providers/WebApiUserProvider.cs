using NForum.Core;
using NForum.Core.Abstractions.Providers;
using System;

namespace NForum.Api.Web.Providers {

	public class WebApiUserProvider : IUserProvider {

		public User CurrentUser {
			get {
				// TODO:
				return null;
			}
		}

		public Boolean Authenticated {
			get {
				// TODO:
				return false;
			}
		}
	}
}
