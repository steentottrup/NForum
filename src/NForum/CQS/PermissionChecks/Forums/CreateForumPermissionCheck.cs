using NForum.CQS.Commands.Forums;
using System;
using System.Security.Principal;

namespace NForum.CQS.PermissionChecks.Forums {

	public class CreateForumPermissionCheck : CommandPermissionCheckBase<CreateForumCommand> {
		protected override Int32 ErrorCode { get; set; }
		protected override String ErrorMessage { get; set; }

		public CreateForumPermissionCheck() {
			// TODO: Get localized text !?!?!
			this.ErrorCode = - 1;
			this.ErrorMessage = "meh";
		}

		protected override Boolean CheckPermissions(CreateForumCommand command, IPrincipal user) {
			// TODO:

			return false;
		}
	}
}
