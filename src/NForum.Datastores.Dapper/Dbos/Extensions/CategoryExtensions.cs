using NForum.Core.Dtos;
using System;

namespace NForum.Datastores.Dapper.Dbos {

	public static class CategoryExtensions {

		public static ICategoryDto ToDto(this Dbos.Category category) {
			return new Dtos.Category {
				Id = category.Id.ToString(),
				Name = category.Name,
				Description = category.Description,
				SortOrder = category.SortOrder
				//CustomProperties = category.CustomProperties
			};
		}
	}
}
