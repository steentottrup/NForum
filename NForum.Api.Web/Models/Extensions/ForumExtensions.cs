using NForum.Core;
using System;

namespace NForum.Api.Web.Models {

	public static class ForumExtensions {

		public static ForumRead ToModel(this Forum forum) {
			return new ForumRead {
				Id = forum.Id,
				Name = forum.Name,
				Description = forum.Description,
				SortOrder = forum.SortOrder
				// TODO:
			};
		}
	}
}
