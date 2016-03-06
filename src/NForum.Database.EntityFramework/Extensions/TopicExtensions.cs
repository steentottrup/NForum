using NForum.Core;
using System;

namespace NForum.Database.EntityFramework {

	public static class TopicExtensions {

		public static Topic ToModel(this Dbos.Topic topic) {
			return new Topic {
				Id = topic.Id.ToString(),
				Subject = topic.Message.Subject,
				Text = topic.Message.Text,
				Created = topic.Message.Created,
				Updated = topic.Message.Updated,
				State = topic.State,
				Type = topic.Type
			};
		}
	}
}
