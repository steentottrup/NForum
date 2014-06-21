using System;

namespace NForum.Core.Events.Payloads {

	public class PostStateUpdated {

		public PostStateUpdated(Post original) {
			this.OriginalPost = original;
		}

		public Post OriginalPost { get; private set; }
		public Post UpdatedPost { get; set; }
	}
}