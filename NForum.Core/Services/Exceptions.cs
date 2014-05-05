using System;

namespace NForum.Core.Services {

	public class PermissionException : ApplicationException {

		public PermissionException(String requiredAccess)
			: base(requiredAccess) {
		}
	}
}