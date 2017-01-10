using NForum.Core.Dtos;
using System;

namespace NForum.Datastores.MongoDB.Dbos {

	public static class CategoryExtensions {

		public static ICategoryDto ToDto(this Dbos.Category category) {
			return new Dtos.Category {
				Id = category.Id.ToString(),
				Description = category.Description,
				Name = category.Name,
				SortOrder = category.SortOrder
				// TODO:
			};
		}
	}
}
