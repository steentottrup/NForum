using NForum.Core;
using System;

namespace NForum.Database.EntityFramework {

	public static class ForumExtensions {

		public static Forum ToModel(this Dbos.Forum forum) {
			return new Forum {
				Id = forum.Id.ToString(),
				CategoryId = forum.CategoryId.ToString(),
				CustomData = forum.CustomData,
				Description = forum.Description,
				Name = forum.Name,
				SortOrder = forum.SortOrder,
				ParentForumId = forum.ParentForumId.ToString()
			};
		}
	}
}
