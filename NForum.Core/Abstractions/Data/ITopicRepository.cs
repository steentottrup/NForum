using System;
using System.Collections.Generic;

namespace NForum.Core.Abstractions.Data {

	public interface ITopicRepository : IRepository<Topic> {
		IEnumerable<Topic> Read(Forum forum, Int32 perPage, Int32 pageIndex);
		IEnumerable<Topic> Read(Forum forum, Int32 perPage, Int32 pageIndex, Boolean includeQuarantined, Boolean includeDeleted);
		Topic BySubject(String subject);
		Topic GetLatest(IEnumerable<Forum> forums);
	}
}