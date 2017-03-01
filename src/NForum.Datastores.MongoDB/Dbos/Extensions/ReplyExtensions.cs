using NForum.Core.Dtos;
using System;

namespace NForum.Datastores.MongoDB.Dbos {

	public static class ReplyExtensions {

		public static IReplyDto ToDto(this Dbos.Reply reply) {
			return new Dtos.Reply {
				Id = reply.Id.ToString()
				// TODO:
			};
		}
	}
}
