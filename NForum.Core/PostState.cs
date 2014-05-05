using System;

namespace NForum.Core {

	public enum PostState {
		None,
		/// <summary>
		/// A deleted post, it can be shown or it can be hidden, depending on the forum settings or the user's settings (if the user is a moderator).
		/// </summary>
		Deleted = 1,
		/// <summary>
		/// A quarantined post, this post will only be visible for moderators!
		/// </summary>
		Quarantined = 2
	}
}