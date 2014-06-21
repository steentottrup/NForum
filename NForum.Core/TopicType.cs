using System;

namespace NForum.Core {

	public enum TopicType {
		/// <summary>
		/// A regular standard topic, shown after announcements and stickies.
		/// </summary>
		Regular = 2,
		/// <summary>
		/// A sticky topic, this topic will "stick" to the top of the list of topics, above regular topics.
		/// </summary>
		Sticky = 1,
		/// <summary>
		/// An announcement topic, this topic will be at the very top of the topic list, above stickies.
		/// Announcement will be shown at the very top of each page in the forum.
		/// </summary>
		Announcement = 0
	}
}