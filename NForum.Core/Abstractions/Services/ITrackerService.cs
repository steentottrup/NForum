using System;
using System.Collections.Generic;

namespace NForum.Core.Abstractions.Services {

	public interface ITrackerService {
		ForumTracker GetTrackingInfo(User user, Forum forum);
		TopicTracker GetTrackingInfo(User user, Topic topic);
		//IEnumerable<ForumTracker> GetTrackingInfo(User user, IEnumerable<Forum> forums);
		//IEnumerable<TopicTracker> GetTrackingInfo(User user, IEnumerable<Topic> topics);
		void UpdateForumTracking(User user, Forum forum);
		//void UpdateTopicTracking(User user, Forum forum);
		void UpdateTracking(User user, Topic topic);
		//void UpdateTracking(User user, IEnumerable<Forum> forums);
	}
}