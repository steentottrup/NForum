using NForum.Core;
using System;

namespace NForum.Api.Web.Models {

	public static class CategoryExtensions {

		public static CategoryRead ToModel(this Category category) {
			return new CategoryRead {
				Id = category.Id,
				Name = category.Name,
				Description = category.Description,
				SortOrder = category.SortOrder
				// TODO: Custom properties!
			};
		}
	}
}