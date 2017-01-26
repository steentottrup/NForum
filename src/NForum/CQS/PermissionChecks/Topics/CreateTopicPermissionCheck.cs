using NForum.CQS.Commands.Topics;
using System;
using System.Security.Principal;

namespace NForum.CQS.PermissionChecks.Topics {

	public class CreateTopicPermissionCheck : CommandPermissionCheckBase<CreateTopicCommand> {
		protected override Int32 ErrorCode { get; set; }
		protected override String ErrorMessage { get; set; }

		protected override Boolean CheckPermissions(CreateTopicCommand command, IPrincipal user) {

			if (command.Type != Domain.TopicType.Regular) {
				// TODO: Requires special permissions!
			}


			return true;
		}
	}
}
