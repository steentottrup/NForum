using NForum.Core.Abstractions.Data;
using NForum.Core.Abstractions.Services;
using System;
using System.Collections.Generic;

namespace NForum.Core.Services {

	public class ForumUserService : IForumUserService {
		protected readonly IDataStore dataStore;

		public ForumUserService(IDataStore dataStore) {
			this.dataStore = dataStore;
		}

		public ForumUser Create(String userName, String emailAddress, String fullname, String userId, String culture, String timezone) {
			// TODO:
			return this.dataStore.CreateForumUser(userName, emailAddress, fullname, userId, culture, timezone);
		}

		public IEnumerable<ForumUser> FindAll() {
			return this.dataStore.FindAllForumUsers();
		}

		public ForumUser FindById(String forumUserId) {
			// TODO:
			return this.dataStore.FindForumUserById(forumUserId);
		}
	}
}
