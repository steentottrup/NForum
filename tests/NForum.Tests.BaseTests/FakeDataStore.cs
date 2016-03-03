using NForum.Core.Abstractions.Data;
using System;
using NForum.Core;
using System.Collections.Generic;
using System.Linq;

namespace NForum.Tests.BaseTests {

	public class FakeDataStore : IDataStore {
		private List<Category> categories = new List<Category>();

		public Category CreateCategory(String name, Int32 sortOrder, String description) {
			Category output = new Category {
				Id = Guid.NewGuid().ToString(),
				Name = name,
				SortOrder = sortOrder,
				Description = description
			};

			categories.Add(output);

			return output;
		}

		public Forum CreateForum(string categoryId, string name, int sortOrder, string description) {
			throw new NotImplementedException();
		}

		public Forum CreateSubForum(string parentForumId, string name, int sortOrder, string description) {
			throw new NotImplementedException();
		}

		public Topic CreateTopic(string forumId, string subject, string text, TopicType type) {
			throw new NotImplementedException();
		}

		public Boolean DeleteCategory(String categoryId) {
			Category cat = this.FindCategoryById(categoryId);
			if (cat != null) {
				this.categories.Remove(cat);
				return true;
			}
			return false;
		}

		public bool DeleteForum(string forumId) {
			throw new NotImplementedException();
		}

		public IEnumerable<Category> FindAll() {
			throw new NotImplementedException();
		}

		public IEnumerable<Topic> FindByForum(string forumId, int pageIndex, int pageSize) {
			throw new NotImplementedException();
		}

		public IEnumerable<Category> FindCategoriesPlus2Levels() {
			throw new NotImplementedException();
		}

		public Category FindCategoryById(String categoryId) {
			return categories.SingleOrDefault(c => c.Id == categoryId);
		}

		public Category FindCategoryPlus2Levels(string categoryId) {
			throw new NotImplementedException();
		}

		public Forum FindForumById(string forumId) {
			throw new NotImplementedException();
		}

		public Forum FindForumPlus2Levels(string forumId) {
			throw new NotImplementedException();
		}

		public Category UpdateCategory(String categoryId, String name, Int32 sortOrder, String description) {
			Category output = this.FindCategoryById(categoryId);

			output.Name = name;
			output.SortOrder = sortOrder;
			output.Description = description;

			return output;
		}

		public Forum UpdateForum(string forumId, string name, int sortOrder, string description) {
			throw new NotImplementedException();
		}
	}
}
