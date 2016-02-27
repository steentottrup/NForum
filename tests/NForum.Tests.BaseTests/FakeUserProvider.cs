using NForum.Core.Abstractions.Providers;
using System;
using NForum.Core.Abstractions;

namespace NForum.Tests.BaseTests {

	public class FakeUserProvider : IUserProvider {
		public IAuthenticatedUser CurrentUser {
			get {
				return new FakeUser();
			}
		}
	}
}
