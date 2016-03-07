using System;
using System.Collections.Generic;

namespace NForum.Core.Abstractions.Data {

	public interface IDataStore {
		/* CRUD for categories */
		Category CreateCategory( String name, Int32 sortOrder, String description);
		Category UpdateCategory(String categoryId, String name, Int32 sortOrder, String description);
		Category FindCategoryById(String categoryId);
		Boolean DeleteCategory(String categoryId);
		/* CRUD for forums */
		Forum CreateForum(String categoryId, String parentForumId, String name, Int32 sortOrder, String description);
		Forum UpdateForum(String forumId, String name, Int32 sortOrder, String description);
		Forum FindForumById(String forumId);
		Boolean DeleteForum(String forumId);
		/* CRUD for topics */
		Topic CreateTopic(String authorId, String forumId, String subject, String text, TopicType type);
		Topic UpdateTopic(String authorId, String topicId, String subject, String text, TopicType type);
		Topic FindTopicById(String topicId);
		Boolean DeleteTopic(String topicId);
		/* CRUD for users */

		/* Methods for UI */
		IEnumerable<Category> FindCategoriesPlus2Levels(/* Permissions/user */);
		Forum FindForumPlus2Levels(/* Permissions/user */String forumId);
		Category FindCategoryPlus2Levels(/* Permissions/user */String categoryId);
		IEnumerable<Topic> FindByForum(String forumId, Int32 pageIndex, Int32 pageSize, Boolean includeExtra);
		Int32 GetNumberOfForumPages(String forumId, Int32 pageSize);
		Int32 GetNumberOfTopicPages(String topicId, Int32 pageSize, Boolean includeDeleted);
		IEnumerable<Reply> FindByTopic(String topicId, Int32 pageIndex, Int32 pageSize, Boolean includeDeleted);

		/* Methods for API/Admin */
		IEnumerable<Category> FindAllCategories(/* Permissions */);
		IEnumerable<Forum> FindAllForums(/* Permissions */);
	}
}
