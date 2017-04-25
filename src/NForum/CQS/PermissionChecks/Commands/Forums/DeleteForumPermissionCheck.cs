using NForum.CQS.Commands.Forums;
using NForum.Infrastructure;
using System;

namespace NForum.CQS.PermissionChecks.Commands.Forums {

	public class DeleteForumPermissionCheck : CommandPermissionCheckForAdminGroupBase<DeleteForumCommand> {

		public DeleteForumPermissionCheck(IBoardConfiguration config) : base(config) {
			// TODO: Get localized text !?!?!
			this.ErrorCode = -1;
			this.ErrorMessage = "meh";
		}
	}
}
