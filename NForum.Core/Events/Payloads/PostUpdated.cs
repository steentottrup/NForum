using System;

namespace NForum.Core.Events.Payloads {

	public class PostUpdated {

		public PostUpdated(Post original) {
			this.OriginalPost = original;
		}

		public Post OriginalPost { get; private set; }
		public Post UpdatedPost { get; set; }
	}
}