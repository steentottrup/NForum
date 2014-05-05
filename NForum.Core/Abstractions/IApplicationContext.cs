using NForum.Core.Abstractions.Data;
using NForum.Core.Abstractions.Providers;
using System;

namespace NForum.Core.Abstractions {

	public interface IApplicationContext {
		IUserProvider UserProvider { get; }
	}
}