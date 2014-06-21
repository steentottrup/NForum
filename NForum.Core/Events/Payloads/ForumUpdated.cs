using System;

namespace NForum.Core.Events.Payloads {

	public class ForumUpdated {

		public ForumUpdated(Forum original) {
			this.OriginalForum = original;
		}

		public Forum OriginalForum { get; private set; }
		public Forum UpdatedForum { get; set; }
	}
}
