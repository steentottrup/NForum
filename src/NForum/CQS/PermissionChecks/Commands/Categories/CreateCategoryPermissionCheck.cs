using NForum.CQS.Commands.Categories;
using NForum.Infrastructure;
using System;

namespace NForum.CQS.PermissionChecks.Commands.Categories {

	public class CreateCategoryPermissionCheck : CommandPermissionCheckForAdminGroupBase<CreateCategoryCommand> {

		public CreateCategoryPermissionCheck(IBoardConfiguration config) : base(config) {
			// TODO: Get localized text !?!?!
			this.ErrorCode = - 1;
			this.ErrorMessage = "meh";
		}
	}
}
