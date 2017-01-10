using NForum.CQS.Commands.Categories;
using System;
using System.Security.Principal;

namespace NForum.CQS.PermissionChecks.Categories {

	public class CreateCategoryPermissionCheck : CommandPermissionCheckBase<CreateCategoryCommand> {
		protected override Int32 ErrorCode { get; set; }
		protected override String ErrorMessage { get; set; }

		public CreateCategoryPermissionCheck() {
			// TODO: Get localized text !?!?!
			this.ErrorCode = - 1;
			this.ErrorMessage = "meh";
		}

		protected override Boolean CheckPermissions(CreateCategoryCommand command, IPrincipal user) {
			// TODO:

			return true;
		}
	}
}
