using NForum.CQS.Commands.Categories;
using System;
using System.Security.Principal;

namespace NForum.CQS.PermissionChecks.Categories {

	public class UpdateCategoryPermissionCheck : CommandPermissionCheckBase<UpdateCategoryCommand> {
		protected override Int32 ErrorCode { get; set; }
		protected override String ErrorMessage { get; set; }

		public UpdateCategoryPermissionCheck() {
			// TODO: Get localized text !?!?!
			this.ErrorCode = -1;
			this.ErrorMessage = "meh";
		}

		protected override Boolean CheckPermissions(UpdateCategoryCommand command, IPrincipal user) {
			// TODO:

			return false;
		}
	}
}
