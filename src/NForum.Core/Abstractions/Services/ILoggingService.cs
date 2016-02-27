using System;

namespace NForum.Core.Abstractions.Services {

	public interface ILoggingService {
		ICoreLogger Core { get; }
		IApplicationLogger Application { get; }
	}
}
