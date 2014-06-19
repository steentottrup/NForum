using System;

namespace NForum.Core.Events.Payloads {

	public class PostStateUpdated {
		public Post OriginalPost { get; set; }
		public Post UpdatedPost { get; set; }
	}
}