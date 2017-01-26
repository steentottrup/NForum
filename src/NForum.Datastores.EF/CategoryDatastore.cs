using System;
using NForum.Core.Dtos;
using NForum.Domain;
using System.Collections.Generic;

namespace NForum.Datastores.EF {

	public class CategoryDatastore : ICategoryDatastore {

		public ICategoryDto Create(Category category) {
			// TODO:
			return new Dtos.Category {
			};
		}

		public void DeleteById(String id) {
			throw new NotImplementedException();
		}

		public void DeleteWithSubElementsById(String Id) {
			throw new NotImplementedException();
		}

		public ICategoriesAndForumsDto ReadAllWithForums() {
			// TODO:
			return new Dtos.CategoriesAndForums {
				Categories = new List<Dtos.Category>(),
				Forums = new List<Dtos.Forum>()
			};
		}

		public ICategoryDto ReadById(String id) {
			throw new NotImplementedException();
		}

		public ICategoryDto Update(Category category) {
			throw new NotImplementedException();
		}
	}
}
