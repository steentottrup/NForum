using NForum.Core;
using NForum.Core.Abstractions;
using NForum.Core.Abstractions.Providers;
using NForum.Core.Abstractions.Services;
using System;
using System.Linq;

namespace NForum.Web.Providers {

	public class MvcUserProvider : IUserProvider {
		protected readonly IForumUserService forumUserService;
		protected ForumUser user = null;

		public MvcUserProvider(IForumUserService forumUserService) {
			this.forumUserService = forumUserService;
		}

		public IAuthenticatedUser CurrentUser {
			get {
				// TODO:
				if (this.user == null) {
					this.user = this.forumUserService.FindAll().FirstOrDefault();
				}
				return this.user == null ? null : new AuthenticatedUser {
					EmailAddress = this.user.EmailAddress,
					Id = this.user.Id,
					Name = this.user.Username,
					IPAddress = System.Net.IPAddress.Parse("127.0.0.1"),
					UserAgent = "cheating"
				};
			}
		}
	}
}
