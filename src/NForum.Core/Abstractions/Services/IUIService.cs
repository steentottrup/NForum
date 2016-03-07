using System;
using System.Collections.Generic;

namespace NForum.Core.Abstractions.Services {

	public interface IUIService {
		IEnumerable<Category> FindCategoriesPlus2Levels();
		Category FindCategoryPlus2Levels(String categoryId);
		Forum FindForumPlus2Levels(String forumId);
		IEnumerable<Topic> FindByForum(String forumId, Int32 pageIndex, Int32 pageSize, Boolean includeExtra = false);
		Int32 GetNumberOfForumPages(String forumId, Int32 pageSize);
		Int32 GetNumberOfTopicPages(String topicId, Int32 pageSize, Boolean includeDeleted);
		IEnumerable<Reply> FindByTopic(String topicId, Int32 pageIndex, Int32 pageSize, Boolean includeDeleted = false);
	}
}
