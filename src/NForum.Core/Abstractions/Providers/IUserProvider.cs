using System;

namespace NForum.Core.Abstractions.Providers {

	public interface IUserProvider {
		IAuthenticatedUser CurrentUser { get; }
	}
}
