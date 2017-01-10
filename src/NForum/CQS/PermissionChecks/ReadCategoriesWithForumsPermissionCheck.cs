using NForum.CQS.Queries;
using NForum.Infrastructure;
using System;
using System.Security.Principal;

namespace NForum.CQS.PermissionChecks {

	public class ReadCategoriesWithForumsPermissionCheck : QueryPermissionCheckBase<ReadCategoriesWithForumsQuery, CategoriesAndForums> {
		private readonly IBoardConfiguration boardConfiguration;

		protected override Int32 ErrorCode { get; set; }
		protected override String ErrorMessage { get; set; }

		public ReadCategoriesWithForumsPermissionCheck(IBoardConfiguration boardConfiguration) {
			this.boardConfiguration = boardConfiguration;
			// TODO: Get localized text !?!?!
			this.ErrorCode = -1;
			this.ErrorMessage = "meh";
		}

		protected override Boolean CheckPermissions(ReadCategoriesWithForumsQuery command, IPrincipal user) {
			// TODO:
			if (boardConfiguration.AllowAnonymousVisitors || user.Identity.IsAuthenticated) {
				return true;
			}

			return false;
		}
	}
}
