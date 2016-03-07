using NForum.Core;
using System;

namespace NForum.Database.EntityFramework {

	public static class ReplyExtensions {

		public static Reply ToModel(this Dbos.Reply reply) {
			return new Reply {
			};
		}
	}
}
