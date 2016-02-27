using NForum.Core.Abstractions;
using System;

namespace NForum.Tests.BaseTests {
	public class FakeUser : IAuthenticatedUser {
		public String Id {
			get {
				return Guid.NewGuid().ToString();
			}
		}

		public String Name {
			get {
				return "Mr. Fake User";
			}
		}
	}
}