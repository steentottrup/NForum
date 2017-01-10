using NForum.CQS.Commands.Forums;
using System;
using System.Security.Principal;

namespace NForum.CQS.PermissionChecks.Categories {

	public class UpdateForumPermissionCheck : CommandPermissionCheckBase<UpdateForumCommand> {
		protected override Int32 ErrorCode { get; set; }
		protected override String ErrorMessage { get; set; }

		public UpdateForumPermissionCheck() {
			// TODO: Get localized text !?!?!
			this.ErrorCode = -1;
			this.ErrorMessage = "meh";
		}

		protected override Boolean CheckPermissions(UpdateForumCommand command, IPrincipal user) {
			// TODO:

			return false;
		}
	}
}
