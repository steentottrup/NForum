using System;
using NForum.Core.Dtos;
using NForum.Domain;
using System.Collections.Generic;

namespace NForum.Datastores.EF {

	public class CategoryDatastore : ICategoryDatastore {

		public ICategoryDto Create(Category category) {
			return new Dtos.Category {
			};
		}

		public ICategoriesAndForumsDto ReadAllWithForums() {
			return new Dtos.CategoriesAndForums {
				Categories = new List<Dtos.Category>(),
				Forums = new List<Dtos.Forum>()
			};
		}
	}
}
