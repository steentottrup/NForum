using System;

namespace NForum.Core.Abstractions {

	public interface IContentCarrier {
		String Id { get; }
		String Subject { get; }
		String Text { get; }
	}
}
