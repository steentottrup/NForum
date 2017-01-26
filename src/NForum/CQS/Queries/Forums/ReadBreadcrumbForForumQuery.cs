using CreativeMinds.CQS.Queries;
using System;

namespace NForum.CQS.Queries.Forums {

	public class ReadBreadcrumbForForumQuery : IQuery<ReadBreadcrumbForForum> {
		public String ForumId { get; set; }
	}
}
