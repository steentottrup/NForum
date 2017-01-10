using System;
using NForum.Core.Dtos;
using MongoDB.Driver;
using NForum.Datastores.MongoDB.Dbos;
using System.Collections.Generic;
using System.Linq;

namespace NForum.Datastores.MongoDB {

	public class CategoryDatastore : ICategoryDatastore {
		private readonly IMongoCollection<Dbos.Category> categories;
		private readonly IMongoCollection<Dbos.CategoryStructure> structure;

		public CategoryDatastore(IMongoCollection<Dbos.Category> categories, IMongoCollection<Dbos.CategoryStructure> structure) {
			this.categories = categories;
			this.structure = structure;
		}

		public ICategoryDto Create(Domain.Category category) {
			Dbos.Category c = new Dbos.Category {
				Name = category.Name,
				Description = category.Description,
				SortOrder = category.SortOrder
			};
			this.categories.InsertOne(c);

			this.structure.InsertOne(new Dbos.CategoryStructure {
				Description = c.Description,
				Id = c.Id,
				Name = c.Name,
				SortOrder = c.SortOrder,
				Forums = new ForumStructure[] { }
			});

			return c.ToDto();
		}

		public ICategoriesAndForumsDto ReadAllWithForums() {
			IEnumerable<Dbos.CategoryStructure> categories = this.structure.Find(d => true).ToList();

			return new Dtos.CategoriesAndForums {
				Categories = categories.Select(c => c.ToDto()),
				Forums = categories.SelectMany(c => c.Forums.Flatten()).Select(f => f.ToDto())
			};
		}
	}
}
