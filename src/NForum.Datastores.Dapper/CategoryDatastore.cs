using System;
using NForum.Core.Dtos;
using NForum.Datastores.Dapper.Dbos;
using NForum.Datastores.Dapper.Repositories;
using System.Collections.Generic;

namespace NForum.Datastores.Dapper {

	public class CategoryDatastore : ICategoryDatastore {
		protected readonly Database database;

		public CategoryDatastore(Database db) {
			this.database = db;
		}

		public ICategoryDto Create(Domain.Category category) {
			return new GenericRepository<Dbos.Category>(this.database).Create(new Dbos.Category {
				Description = category.Description,
				Name = category.Name,
				SortOrder = category.SortOrder
			}).ToDto();
		}

		public void DeleteById(String id) {
			new GenericRepository<Dbos.Category>(this.database).DeleteById(Int32.Parse(id));
		}

		public void DeleteWithSubElementsById(String Id) {
			// TODO:

			throw new NotImplementedException();
		}

		public ICategoriesAndForumsDto ReadAllWithForums() {
			IEnumerable<Dbos.Category> categories = new GenericRepository<Dbos.Category>(this.database).FindAll();
			IEnumerable<Dbos.Forum> forums = new GenericRepository<Dbos.Forum>(this.database).FindAll();

			return null;
		}

		public ICategoryDto ReadById(String id) {
			return new GenericRepository<Dbos.Category>(this.database).FindById(Int32.Parse(id)).ToDto();
		}

		public ICategoryDto Update(Domain.Category category) {
			throw new NotImplementedException();
		}
	}
}
