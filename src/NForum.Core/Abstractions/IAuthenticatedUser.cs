using System;

namespace NForum.Core.Abstractions {

	public interface IAuthenticatedUser {
		String Id { get; }
		String Name { get; }
	}
}
