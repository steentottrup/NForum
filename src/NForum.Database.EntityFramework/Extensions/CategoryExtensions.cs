using NForum.Core;
using System;

namespace NForum.Database.EntityFramework {

	public static class CategoryExtensions {

		public static Category ToModel(this Dbos.Category category) {
			return new Category {
				Id = category.Id.ToString(),
				Name = category.Name,
				Description = category.Description,
				SortOrder = category.SortOrder,
				CustomData = category.CustomData
			};
		}
	}
}
