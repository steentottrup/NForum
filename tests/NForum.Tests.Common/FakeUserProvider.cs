using NForum.Core.Abstractions.Providers;
using System;
using NForum.Core.Abstractions;

namespace NForum.Tests.Common {

	public class FakeUserProvider : IUserProvider {
		private NForum.Database.EntityFramework.Dbos.ForumUser fu;

		public FakeUserProvider(NForum.Database.EntityFramework.Dbos.ForumUser fu) {
			this.fu = fu;
		}

		public IAuthenticatedUser CurrentUser {
			get {
				return new FakeUser {
					Id = fu.Id.ToString(),
					EmailAddress = fu.EmailAddress,
					Name = fu.Username
				};
			}
		}
	}
}
