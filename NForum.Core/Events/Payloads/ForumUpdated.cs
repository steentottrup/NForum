using System;

namespace NForum.Core.Events.Payloads {

	public class ForumUpdated {
		public Forum Forum { get; set; }
		public Forum UpdatedForum { get; set; }
	}
}
