using NForum.CQS.Commands.Forums;
using NForum.Infrastructure;
using System;

namespace NForum.CQS.PermissionChecks.Commands.Categories {

	public class UpdateForumPermissionCheck : CommandPermissionCheckForAdminGroupBase<UpdateForumCommand> {

		public UpdateForumPermissionCheck(IBoardConfiguration config) : base(config) {
			// TODO: Get localized text !?!?!
			this.ErrorCode = -1;
			this.ErrorMessage = "meh";
		}
	}
}
