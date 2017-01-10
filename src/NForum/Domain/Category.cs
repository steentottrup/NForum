using NForum.Core.Dtos;
using NForum.Domain.Abstractions;
using System;

namespace NForum.Domain {

	public class Category : StructureElement {

		public Category(ICategoryDto data) : base(data) { }

		public Category(String name, Int32 sortOrder, String description) : base(name, sortOrder, description) { }
	}
}
