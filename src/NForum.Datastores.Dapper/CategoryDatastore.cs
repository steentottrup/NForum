using System;
using NForum.Core.Dtos;
using NForum.Datastores.Dapper.Dbos;
using System.Data;

namespace NForum.Datastores.Dapper {

	public class CategoryDatastore : GenericRepository<Dbos.Category>, ICategoryDatastore {

		public CategoryDatastore(IDbConnection connection) : base(connection) { }

		public ICategoryDto Create(Domain.Category category) {
			return this.Create(new Dbos.Category {
				Description = category.Description,
				Name = category.Name,
				SortOrder = category.SortOrder
			}).ToDto();
		}

		public void DeleteById(String id) {
			this.DeleteById(Int32.Parse(id));
		}

		public void DeleteWithSubElementsById(String Id) {
			throw new NotImplementedException();
		}

		public ICategoriesAndForumsDto ReadAllWithForums() {
			throw new NotImplementedException();
		}

		public ICategoryDto ReadById(String id) {
			return this.FindById(Int32.Parse(id)).ToDto();
		}

		public ICategoryDto Update(Domain.Category category) {
			return this.Update(new Dbos.Category {
				Id = Int32.Parse(category.Id),
				Name = category.Name,
				Description = category.Description,
				SortOrder = category.SortOrder
			}).ToDto();
		}
	}
}
