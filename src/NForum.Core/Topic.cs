using NForum.Core.Abstractions;
using System;

namespace NForum.Core {

	public class Topic : IContentCarrier, IAuthoredContent {
		public String Id { get; set; }
		public String Subject { get; set; }
		public String Text { get; set; }

		public TopicType Type { get; set; }
		public TopicState State { get; set; }

		public DateTime Created { get; set; }
		public DateTime Updated { get; set; }
		public String CreatorId { get; set; }
		public String EditorId { get; set; }

		public DateTime? LatestReplyDatestamp { get; set; }
		public Reply LatestReply { get; set; }
	}
}
