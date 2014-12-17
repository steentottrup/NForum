using System;

namespace NForum.Core {

	public class TopicHistory {
		public Int32 Id { get; set; }
		public Int32 ForumId { get; set; }
		public Int32 TopicId { get; set; }
		public DateTime Timestamp { get; set; }

		public virtual Forum Forum { get; set; }
		public virtual Topic Topic { get; set; }
	}
}