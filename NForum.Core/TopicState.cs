using System;

namespace NForum.Core {

	public enum TopicState {
		None = 0,
		/// <summary>
		/// A locked topic, no more posts can be posted in a locked topic!
		/// </summary>
		Locked = 1,
		/// <summary>
		/// A quarantined topic, this topic will only be visible for moderators!
		/// </summary>
		Quarantined = 2,
		/// <summary>
		/// A deleted topic.
		/// </summary>
		Deleted = 4,
		/// <summary>
		/// A moved topic.
		/// </summary>
		Moved = 8
	}
}