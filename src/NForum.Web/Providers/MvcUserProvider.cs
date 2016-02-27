using NForum.Core.Abstractions.Providers;
using System;
using NForum.Core.Abstractions;

namespace NForum.Web.Providers {

	public class MvcUserProvider : IUserProvider {

		public IAuthenticatedUser CurrentUser {
			get {
				// TODO:
				return null;
			}
		}
	}
}
