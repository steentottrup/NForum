using System;

namespace NForum.Core.Abstractions {

	public interface IApplicationContext {
		IAuthenticatedUser UserProvider { get; }
		IState State { get; }
	}
}
