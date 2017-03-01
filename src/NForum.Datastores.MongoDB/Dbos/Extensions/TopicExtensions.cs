using NForum.Core.Dtos;
using System;

namespace NForum.Datastores.MongoDB.Dbos {

	public static class TopicExtensions {

		public static ITopicDto ToDto(this Dbos.Topic topic) {
			return new Dtos.Topic {
				Id = topic.Id.ToString()
				// TODO:
			};
		}
	}
}
