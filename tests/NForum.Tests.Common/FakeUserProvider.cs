using NForum.Core.Abstractions.Providers;
using System;
using NForum.Core.Abstractions;

namespace NForum.Tests.Common {

	public class FakeUserProvider : IUserProvider {
		public IAuthenticatedUser CurrentUser {
			get {
				return new FakeUser();
			}
		}
	}
}
