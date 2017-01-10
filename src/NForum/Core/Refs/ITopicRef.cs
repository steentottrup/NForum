using System;

namespace NForum.Core.Refs {

	public interface ITopicRef {
		String Id { get; }
		String Subject { get; }
	}
}
