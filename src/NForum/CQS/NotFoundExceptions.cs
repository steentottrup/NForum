using System;

namespace NForum.CQS {

	public class CategoryNotFoundException : Exception {
		public CategoryNotFoundException(String categoryId) : base($"Could not locate a category with the Id {categoryId}.") { }
	}

	public class ForumNotFoundException : Exception {
		public ForumNotFoundException(String forumId) : base($"Could not locate a forum with the Id {forumId}.") { }
	}

	public class TopicNotFoundException : Exception {
		public TopicNotFoundException(String topicId) : base($"Could not locate a topic with the Id {topicId}.") { }
	}
}
