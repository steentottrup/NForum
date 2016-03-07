using System;

namespace NForum.Core.Abstractions {

	public interface ISettings {
		Int32 TopicsPerPage { get; }
		Int32 RepliesPerPage { get; }
	}
}
