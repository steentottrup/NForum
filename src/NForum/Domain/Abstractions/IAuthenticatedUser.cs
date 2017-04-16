using System;

namespace NForum.Domain.Abstractions {

	public interface IAuthenticatedUser {
		String GetId();
	}
}
