using NForum.Core;
using System;

namespace NForum.Demo.WebApi.Models {

	public static class CategoryExtensions {

		public static CategoryRead ToModel(this Category category) {
			return new CategoryRead {
				Id = category.Id,
				Name = category.Name,
				 Description = category.Description,
				 SortOrder = category.SortOrder
			};
		}
	}
}