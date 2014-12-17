using System;

namespace NForum.Core {

	public static class TopicExtensions {

		/// <summary>
		/// Method for checking if a topic should be marked unread/new posts.
		/// </summary>
		/// <param name="t">The topic in question.</param>
		/// <param name="tt">A TopicTracker, if any exists.</param>
		/// <param name="user">The current user, null if unauthenticated.</param>
		/// <returns>Returns true if posts have been created since the user last viewed the topic, else false.</returns>
		public static Boolean UnRead(this Topic t, TopicTracker tt, User user) {
			// No user, or topic created later then last viewed (does that make sense?), or latest post created before last view?
			// TODO:
			//if (user == null ||
			//	(tt != null && 
			//		(tt.LastViewed >= t.Created ||
			//			(t.LatestPost != null && tt.LastViewed >= t.LatestPost.Created)
			//		)
			//	)) {

			//	return false;
			//}
			return true;
		}

		/// <summary>
		/// Method for checking if a topic has "gone public".
		/// </summary>
		/// <param name="t">The topic in question.</param>
		/// <param name="oldTopic">The topic with the old state.</param>
		/// <returns>True if the TopicState on the topic has gone from Deletd/Quarantined to None, Moved or Locked, else false.</returns>
		public static Boolean GonePublic(this Topic t, Topic oldTopic) {
			return !oldTopic.IsVisible() && t.IsVisible();
		}

		/// <summary>
		/// Method for checking if a topic is an announcement.
		/// </summary>
		/// <param name="t">The topic in question.</param>
		/// <returns>Returns true if the topic has the Announcement state, otherwise false.</returns>
		public static Boolean IsAnnouncement(this Topic t) {
			return t.Type == TopicType.Announcement;
		}

		/// <summary>
		/// Method for checking if a topic is a sticky.
		/// </summary>
		/// <param name="t">The topic in question.</param>
		/// <returns>Returns true if the topic has the Sticky state, otherwise false.</returns>
		public static Boolean IsSticky(this Topic t) {
			return t.Type == TopicType.Sticky;
		}

		public static Boolean IsVisible(this Topic t) {
			return t.State != TopicState.Deleted && t.State != TopicState.Quarantined;
		}
	}
}