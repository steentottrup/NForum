using System;

namespace NForum.Core {

	public static class TopicExtensions {

		public static Boolean IsAnnouncement(this Topic t) {
			return t.Type == TopicType.Announcement;
		}

		public static Boolean IsSticky(this Topic t) {
			return t.Type == TopicType.Sticky;
		}

		public static Boolean IsVisible(this Topic t) {
			return t.State == TopicState.Locked || t.State == TopicState.Moved || t.State == TopicState.None;
		}
	}
}