using System;

namespace NForum.Core.Abstractions {

	public interface IReport {
		DateTime Timestamp { get; set; }
		String Reason { get; set; }
		User Reporter { get; set; }
		User Resolver { get; set; }
		Boolean Resolved { get; set; }
		DateTime? ResolvedTimestamp { get; set; }
	}
}
