using System;

namespace NForum.Core.Events.Payloads {

	public class PostStateUpdated {
		public Post Post { get; set; }
		public Post UpdatedPost { get; set; }
	}
}