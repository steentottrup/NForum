using NForum.Core;
using System;

namespace NForum.Database.EntityFramework {

	public static class ReplyExtensions {

		public static Reply ToModel(this Dbos.Reply reply) {
			return new Reply {
				Created = reply.Message.Created,
				CreatorId = reply.Message.AuthorId.ToString(),
				EditorId = reply.Message.EditorId.ToString(),
				Updated = reply.Message.Updated,
				Id = reply.Id.ToString(),
				State = reply.State,
				Subject = reply.Message.Subject,
				Text = reply.Message.Text
			};
		}
	}
}
