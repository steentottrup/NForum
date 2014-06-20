using System;
using System.Collections.Generic;

namespace NForum.Core.Abstractions.Services {

	public interface IFollowerService {
		IEnumerable<FollowForum> GetFollowers(Forum forum);
		IEnumerable<FollowTopic> GetFollowers(Topic topic);
		void Follow(Forum forum);
		void Follow(Topic topic);
		void UnFollow(Forum forum);
		void UnFollow(Topic topic);
	}
}