using System;
using System.Collections.Generic;

namespace NForum.Core.Abstractions.Services {

	public interface ITopicService {
		Topic Create(Forum forum, String subject, String message);
		Topic Create(Forum forum, String subject, String message, TopicType type);

		Topic Read(Int32 id);
		Topic Read(String name);
		IEnumerable<Topic> Read(Forum forum, Int32 pageIndex);
		IEnumerable<Topic> Read(Forum forum, Int32 pageIndex, Boolean includeQuarantined, Boolean includeDeleted);

		Topic Update(Topic topic);

		void Delete(Topic topic);
	}
}