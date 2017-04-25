using NForum.CQS.Commands.Topics;
using NForum.Domain.Abstractions;
using NForum.Infrastructure;
using System;
using System.Security.Principal;

namespace NForum.CQS.PermissionChecks.Commands.Topics {

	public class CreateTopicPermissionCheck : CommandPermissionCheckBase<CreateTopicCommand> {
		protected readonly IElementPermissionCheck permissions;
		protected override Int32 ErrorCode { get; set; }
		protected override String ErrorMessage { get; set; }

		public CreateTopicPermissionCheck(IElementPermissionCheck permissions) : base() {
			this.permissions = permissions;
			// TODO: Get localized text !?!?!
			this.ErrorCode = -1;
			this.ErrorMessage = "meh";
		}

		protected override Boolean CheckPermissions(CreateTopicCommand command, IPrincipal user) {
			// Do the user have "post" permissions in the given forum?
			if (!this.permissions.HasPermissions(command.ForumId, Domain.Permissions.Post, user)) {
				return false;
			}
			// If the topic is not a "regular" topic, do the user have permissions to set "priority" in the given forum?
			if (command.Type != Domain.TopicType.Regular && !this.permissions.HasPermissions(command.ForumId, Domain.Permissions.Priority, user)) {
				return false;
			}

			return true;
		}
	}
}
