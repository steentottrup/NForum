using NForum.Core.Abstractions;
using System;

namespace NForum.Core {

	public class Reply : IContentCarrier, IAuthoredContent {
		public String Id { get; set; }
		public String Subject { get; set; }
		public String Text { get; set; }
		public ReplyState State { get; set; }

		public DateTime Created { get; set; }
		public DateTime Updated { get; set; }
		public String CreatorId { get; set; }
		public String EditorId { get; set; }
	}
}
