using System;

namespace NForum.Core.Abstractions {

	public interface IAuthoredContent {
		DateTime Created { get; }
		DateTime Updated { get; }
		String CreatorId { get; }
		String EditorId { get; }
	}
}
