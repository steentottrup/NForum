using NForum.Core.Abstractions;
using System;

namespace NForum.Core.Services {

	public class PermissionException : Exception {

		public PermissionException(String message, IAuthenticatedUser user) : base(message) { }
	}
}
