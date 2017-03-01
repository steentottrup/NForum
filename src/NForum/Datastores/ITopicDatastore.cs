using NForum.Core.Dtos;
using NForum.Domain;
using System;

namespace NForum.Datastores {

	public interface ITopicDatastore {
		ITopicDto Create(Topic topic);
		ITopicDto ReadById(String id);
		ITopicDto Update(Topic topic);
		void DeleteById(String id);
		void Move(String topicId, String destinationForumId);
	}
}
