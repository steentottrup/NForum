using System;

namespace NForum.Core.Abstractions {

	public class ITracker {
		User User { get; set; }
		DateTime LastViewed { get; set; }
	}
}