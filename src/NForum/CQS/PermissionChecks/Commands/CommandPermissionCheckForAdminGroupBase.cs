using CreativeMinds.CQS.Commands;
using NForum.Infrastructure;
using System;
using System.Security.Principal;

namespace NForum.CQS.PermissionChecks.Commands {

	public class CommandPermissionCheckForAdminGroupBase<TCommand> : CommandPermissionCheckBase<TCommand> where TCommand : ICommand {
		protected readonly IBoardConfiguration boardConfiguration;

		protected override Int32 ErrorCode { get; set; }
		protected override String ErrorMessage { get; set; }

		public CommandPermissionCheckForAdminGroupBase(IBoardConfiguration boardConfiguration) {
			this.boardConfiguration = boardConfiguration;
		}

		protected override Boolean CheckPermissions(TCommand command, IPrincipal user) {
			// TODO:
			if (this.boardConfiguration.AllowAnonymousVisitors || user.Identity.IsAuthenticated) {
				return true;
			}

			return false;
		}
	}
}
