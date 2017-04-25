using NForum.CQS.Queries.Forums;
using NForum.Domain.Abstractions;
using NForum.Infrastructure;
using System;
using System.Security.Principal;

namespace NForum.CQS.PermissionChecks.Queries {

	public class ReadBreadcrumbForForumPermissionCheck : QueryPermissionCheckBase<ReadBreadcrumbForForumQuery, ReadBreadcrumbForForum> {
		protected readonly IBoardConfiguration boardConfiguration;
		protected readonly IElementPermissionCheck elementCheck;

		protected override Int32 ErrorCode { get; set; }
		protected override String ErrorMessage { get; set; }

		public ReadBreadcrumbForForumPermissionCheck(IBoardConfiguration boardConfiguration, IElementPermissionCheck elementCheck) {
			this.boardConfiguration = boardConfiguration;
			this.elementCheck = elementCheck;
			// TODO: Get localized text !?!?!
			this.ErrorCode = -1;
			this.ErrorMessage = "meh";
		}

		protected override Boolean CheckPermissions(ReadBreadcrumbForForumQuery command, IPrincipal user) {
			// Let's just make sure the user can actually read the forum in question!
			return this.elementCheck.HasPermissions(command.ForumId, Domain.Permissions.Read, user);
		}
	}
}
