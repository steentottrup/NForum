using NForum.Core;
using NForum.Core.Abstractions.Providers;
using System;

namespace NForum.Tests.CommonMocks {

	public class DummyUserProvider : IUserProvider {
		private readonly User user;

		public DummyUserProvider(User user) {
			this.user = user;
		}

		public User CurrentUser {
			get {
				return user;
			}
		}

		public Boolean Authenticated {
			get {
				return true;
			}
		}
	}
}