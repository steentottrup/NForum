using System;

namespace NForum.Core.Abstractions {

	public interface ITracker {
		User User { get; set; }
		DateTime LastViewed { get; set; }
	}
}