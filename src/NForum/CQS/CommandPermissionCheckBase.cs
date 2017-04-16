using CreativeMinds.CQS.Commands;
using CreativeMinds.CQS.Permissions;
using NForum.Domain.Abstractions;
using NForum.Infrastructure;
using System;
using System.Security.Principal;

namespace NForum.CQS {

	public abstract class CommandPermissionCheckBase<TCommand> : IPermissionCheck<TCommand> where TCommand : ICommand {
		protected abstract String ErrorMessage { get; set; }
		protected abstract Int32 ErrorCode { get; set; }

		protected CommandPermissionCheckBase() { }

		protected abstract Boolean CheckPermissions(TCommand command, IPrincipal user);

		public virtual Tuple<Boolean, String, Int32> Check(TCommand command, IPrincipal user) {
			Boolean hasPermissions = this.CheckPermissions(command, user);
			return new Tuple<Boolean, String, Int32>(
				hasPermissions,
				hasPermissions ? String.Empty : this.ErrorMessage,
				hasPermissions ? 0 : this.ErrorCode
			);
		}
	}
}
