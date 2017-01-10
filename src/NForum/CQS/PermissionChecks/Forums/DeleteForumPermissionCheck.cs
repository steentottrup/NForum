using NForum.CQS.Commands.Forums;
using System;
using System.Security.Principal;

namespace NForum.CQS.PermissionChecks.Forums {

	public class DeleteForumPermissionCheck : CommandPermissionCheckBase<DeleteForumCommand> {
		protected override Int32 ErrorCode { get; set; }

		protected override String ErrorMessage { get; set; }

		public DeleteForumPermissionCheck() {
			// TODO: Get localized text !?!?!
			this.ErrorCode = -1;
			this.ErrorMessage = "meh";
		}

		protected override Boolean CheckPermissions(DeleteForumCommand command, IPrincipal user) {
			// TODO:

			return false;
		}
	}
}
