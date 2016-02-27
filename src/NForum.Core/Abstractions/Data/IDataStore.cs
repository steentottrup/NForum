using System;
using System.Collections.Generic;

namespace NForum.Core.Abstractions.Data {

	public interface IDataStore {
		/* CRUD for categories */
		Category CreateCategory(String name, Int32 sortOrder, String description);
		Category UpdateCategory(String categoryId, String name, Int32 sortOrder, String description);
		Category FindCategoryById(String categoryId);
		Boolean DeleteCategory(String categoryId);
		/* CRUD for forums */
		Forum CreateForum(String categoryId, String name, Int32 sortOrder, String description);
		Forum CreateSubForum(String parentForumId, String name, Int32 sortOrder, String description);
		Forum UpdateForum(String forumId, String name, Int32 sortOrder, String description);
		Forum FindForumById(String forumId);
		Boolean DeleteForum(String forumId);
		/* Methods for UI */
		IEnumerable<Category> FindCategoriesPlus2Levels(/* Permissions/user */);
		Forum FindForumPlus2Levels(/* Permissions/user */String forumId);
		Category FindCategoryPlus2Levels(/* Permissions/user */String categoryId);

		/* Methods for API/Admin */
		IEnumerable<Category> FindAll(/* Permissions */);
	}
}
