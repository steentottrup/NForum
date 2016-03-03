using System;
using System.Collections.Generic;

namespace NForum.Core.Abstractions.Services {

	public interface ITopicService {
		Topic Create(String forumId, String subject, String text, TopicType? type = null);
		Topic FindById(String topicId);
		IEnumerable<Topic> FindAll();
		Topic Update(String topicId, String subject, String text, TopicType? type = null);
		Boolean Delete(String topicId);

		IEnumerable<Topic> FindByForum(String forumId, Int32 pageIndex, Int32 pageSize);
	}
}
