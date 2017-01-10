using CreativeMinds.CQS.Permissions;
using CreativeMinds.CQS.Queries;
using System;
using System.Security.Principal;

namespace NForum.CQS {

	public abstract class QueryPermissionCheckBase<TQuery, TResult> : IPermissionCheck<TQuery> where TQuery : IQuery<TResult> {
		protected abstract String ErrorMessage { get; set; }
		protected abstract Int32 ErrorCode { get; set; }

		protected abstract Boolean CheckPermissions(TQuery query, IPrincipal user);

		public virtual Tuple<Boolean, String, Int32> Check(TQuery query, IPrincipal user) {
			Boolean hasPermissions = this.CheckPermissions(query, user);
			return new Tuple<Boolean, String, Int32>(
				hasPermissions,
				hasPermissions ? String.Empty : this.ErrorMessage,
				hasPermissions ? 0 : this.ErrorCode
			);
		}
	}
}
