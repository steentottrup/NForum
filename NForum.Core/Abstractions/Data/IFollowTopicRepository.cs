using System;
using System.Collections.Generic;

namespace NForum.Core.Abstractions.Data {

	public interface IFollowTopicRepository : IRepository<FollowTopic> {
		IEnumerable<FollowTopic> ByTopic(Topic topic);
		FollowTopic ByUserAndTopic(Topic topic, User user);
	}
}