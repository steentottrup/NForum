using System;

namespace NForum.Core {

	public class TopicVersion : BaseTopic {
		/// <summary>
		/// The unique identifier of the topic version
		/// </summary>
		public Int32 Id { get; set; }

		/// <summary>
		/// The unique identifier of the original topic
		/// </summary>
		public Int32 TopicId { get; set; }
	}
}