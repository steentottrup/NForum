using NForum.Domain.Abstractions;
using System;
using System.Security.Principal;

namespace NForum.Infrastructure {

	public interface IUserProvider {
		IAuthenticatedUser Get(IPrincipal user);
		IAuthor GetAuthor(IPrincipal user);
	}
}
