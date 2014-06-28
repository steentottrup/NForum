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
			if (user == null ||
				(ft != null && ((f.LatestTopic != null && (ft.LastViewed >= f.LatestTopic.Created)) ||
					(f.LatestPost != null && (ft.LastViewed >= f.LatestPost.Created))))
				) {

				return false;
			}
			return true;
		}
	}
}