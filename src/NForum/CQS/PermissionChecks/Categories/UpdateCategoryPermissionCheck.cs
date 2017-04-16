﻿using NForum.CQS.Commands.Categories;
using NForum.Domain.Abstractions;
using NForum.Infrastructure;
using System;
using System.Security.Principal;

namespace NForum.CQS.PermissionChecks.Categories {

	public class UpdateCategoryPermissionCheck : CommandPermissionCheckBase<UpdateCategoryCommand> {
		protected readonly IBoardConfiguration config;
		protected override Int32 ErrorCode { get; set; }
		protected override String ErrorMessage { get; set; }

		public UpdateCategoryPermissionCheck(IBoardConfiguration config) : base() {
			this.config = config;
			// TODO: Get localized text !?!?!
			this.ErrorCode = -1;
			this.ErrorMessage = "meh";
		}

		protected override Boolean CheckPermissions(UpdateCategoryCommand command, IPrincipal user) {
			// Only users in the "Admin" group are allowed to perform this action!
			if (!user.IsInRole(this.config.GetAdminGroupName())) {
				return false;
			}
			// TODO:

			return false;
		}
	}
}
