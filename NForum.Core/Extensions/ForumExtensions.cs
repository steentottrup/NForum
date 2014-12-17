using NForum.Core.Abstractions.Data;
using System;

namespace NForum.Core {

	public static class ForumExtensions {

		/// <summary>
		/// Method for checking if a forum should be marked unread/new topics/posts.
		/// </summary>
		/// <param name="f">The forum in question.</param>
		/// <param name="ft">A ForumTracker, if any exists.</param>
		/// <param name="user">The current user, null if unauthenticated.</param>
		/// <returns>Returns true if posts/topics have been created since the user last viewed the forum, else false.</returns>
		public static Boolean UnRead(this Forum f, ForumTracker ft, User user) {
			// TODO:
			//if (user == null ||
			//	(ft != null && ((f.LatestTopic != null && (ft.LastViewed >= f.LatestTopic.Created)) ||
			//		(f.LatestPost != null && (ft.LastViewed >= f.LatestPost.Created))))
			//	) {

			//	return false;
			//}
			return true;
		}

		///// <summary>
		///// Method for updating a forum with the latest post, if the given post is newer.
		///// </summary>
		///// <param name="f">The forum to be updated.</param>
		///// <param name="p">The latest post.</param>
		///// <returns>True if the given post was newer than the existing latest topic/post on the forum.</returns>
		//public static Boolean Updated(this Forum f, Post p) {
		//	if ((f.LatestTopic == null && f.LatestPost == null) ||
		//		(f.LatestTopic == null && f.LatestPost != null && f.LatestPost.Created <= p.Created) ||
		//		(f.LatestTopic != null && f.LatestPost == null && f.LatestTopic.Created <= p.Created)) {

		//		f.LatestPost = p;
		//		f.LatestTopic = null;
		//		return true;
		//	}
		//	return false;
		//}

		///// <summary>
		///// Method for updating a forum with the latest topic, if the given topic is newer.
		///// </summary>
		///// <param name="f">The forum to be updated.</param>
		///// <param name="t">The latest topic.</param>
		///// <param name="topicHistRepo"></param>
		///// <returns>True if the given topic was newer than the existing latest topic/post on the forum.</returns>
		//public static Boolean Updated(this Forum f, Topic t, ITopicHistoryRepository topicHistRepo) {
		//	TopicHistory th = topicHistRepo.ByForum(f);
		//	if (th == null || (th.TopicId != t.Id && th.Topic.Created <= t.Created)) {
		//		// TODO: Clear table for forum?
		//		topicHistRepo.Delete(th);

		//		topicHistRepo.Create(new TopicHistory {
		//			Timestamp = t.Created,
		//			ForumId= f.Id,
		//			TopicId = t.Id
		//		});

		//		return true;
		//	}
		//	//if ((f.LatestTopic == null && f.LatestPost == null) ||
		//	//	(f.LatestTopic == null && f.LatestPost != null && f.LatestPost.Created <= t.Created) ||
		//	//	(f.LatestTopic != null && f.LatestPost == null && f.LatestTopic.Created <= t.Created)) {

		//	//	f.LatestTopic = t;
		//	//	f.LatestPost = null;
		//	//	return true;
		//	//}
		//	return false;
		//}
	}
}