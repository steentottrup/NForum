using NForum.Core.Dtos;
using System;

namespace NForum.Domain {

	public static class CategoryExtensions {

		public static Category ToDomainObject(this ICategoryDto dto) {
				return new Category(dto);
		}
	}
}
