using System;
using System.Collections.Generic;

namespace NForum.Core.Abstractions.Data {

	public interface ITopicTrackerRepository : IRepository<TopicTracker> {
		TopicTracker ByUserAndTopic(User user, Topic topic);
		IEnumerable<TopicTracker> ByUser(User user);
		IEnumerable<TopicTracker> ByUserAndForum(User user, Forum forum);
	}
}