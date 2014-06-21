using System;

namespace NForum.Core.Services {

	public abstract class NForumException : ApplicationException {
		protected NForumException() : base() { }
		protected NForumException(String message) : base(message) { }
	}

	public class NoAuthenticatedUserFoundException : NForumException {
	}

	public class PermissionException : NForumException {

		public PermissionException(String requiredAccess)
			: base(requiredAccess) {
		}
	}
}