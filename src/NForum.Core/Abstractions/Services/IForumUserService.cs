using System;
using System.Collections.Generic;

namespace NForum.Core.Abstractions.Services {

	public interface IForumUserService {
		ForumUser Create(String userName, String emailAddress, String fullname, String userId, String culture, String timezone);
		ForumUser FindById(String forumUserId);
		IEnumerable<ForumUser> FindAll();
	}
}
