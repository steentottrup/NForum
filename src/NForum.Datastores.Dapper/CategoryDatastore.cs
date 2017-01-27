using System;
using NForum.Core.Dtos;
using NForum.Domain;

namespace NForum.Datastores.Dapper {

	public class CategoryDatastore : ICategoryDatastore {

		public CategoryDatastore() {
		}

		public ICategoryDto Create(Category category) {
			throw new NotImplementedException();
		}

		public void DeleteById(String id) {
			throw new NotImplementedException();
		}

		public void DeleteWithSubElementsById(String Id) {
			throw new NotImplementedException();
		}

		public ICategoriesAndForumsDto ReadAllWithForums() {
			throw new NotImplementedException();
		}

		public ICategoryDto ReadById(String id) {
			throw new NotImplementedException();
		}

		public ICategoryDto Update(Category category) {
			throw new NotImplementedException();
		}
	}
}
