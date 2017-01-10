using System;

namespace NForum.Core.Refs {

	public interface IReplyRef {
		String Id { get; }
		String Subject { get; }
	}
}
