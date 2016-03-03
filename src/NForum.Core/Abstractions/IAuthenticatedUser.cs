using System;
using System.Net;

namespace NForum.Core.Abstractions {

	public interface IAuthenticatedUser {
		String Id { get; }
		String Name { get; }
		String EmailAddress { get; }

		String UserAgent { get; }
		IPAddress IPAddress { get; }
	}
}
