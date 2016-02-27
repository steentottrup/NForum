using System;

namespace NForum.Core {

	public enum ReplyState {
		None = 0,
		/// <summary>
		/// A deleted reply, it can be shown or it can be hidden, depending on the forum settings or the user's settings (if the user is a moderator).
		/// </summary>
		Quarantined = 2,
		/// <summary>
		/// A quarantined reply, this reply will only be visible for moderators!
		/// </summary>
		Deleted = 4
	}
}
