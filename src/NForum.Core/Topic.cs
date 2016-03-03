using System;

namespace NForum.Core {

	public class Topic {
		public String Id { get; set; }
		public String Subject { get; set; }
		public String Text { get; set; }
		public DateTime Created { get; set; }
		public DateTime Updated { get; set; }

		public DateTime? LatestReplyDatestamp { get; set; }
		public Reply LatestReply { get; set; }
	}
}
