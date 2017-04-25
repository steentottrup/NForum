using NForum.CQS.Queries;
using NForum.Infrastructure;
using System;
using System.Security.Principal;

namespace NForum.CQS.PermissionChecks.Queries {

	public class ReadCategoriesWithForumsPermissionCheck : QueryPermissionCheckBase<ReadCategoriesWithForumsQuery, CategoriesAndForums> {
		protected readonly IBoardConfiguration boardConfiguration;

		protected override Int32 ErrorCode { get; set; }
		protected override String ErrorMessage { get; set; }

		public ReadCategoriesWithForumsPermissionCheck(IBoardConfiguration boardConfiguration) {
			this.boardConfiguration = boardConfiguration;
			// TODO: Get localized text !?!?!
			this.ErrorCode = -1;
			this.ErrorMessage = "meh";
		}

		protected override Boolean CheckPermissions(ReadCategoriesWithForumsQuery command, IPrincipal user) {
			// We're not going to check each and every category/forum here, that should be done in the actual query!
			// We're just making sure that the board either allows anonymous users, or that the user is authenticated.
			return (this.boardConfiguration.AllowAnonymousVisitors || user.Identity.IsAuthenticated);
		}
	}
}
