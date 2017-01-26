using MongoDB.Bson;
using NForum.Core.Dtos;
using NForum.Datastores.MongoDB.Dbos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NForum.Datastores.MongoDB {

	public class CategoryDatastore : ICategoryDatastore {
		protected readonly CommonDatastore datastore;

		public CategoryDatastore(CommonDatastore datastore) {
			this.datastore = datastore;
		}

		public ICategoryDto Create(Domain.Category category) {
			Dbos.Category c = new Dbos.Category {
				Name = category.Name,
				Description = category.Description,
				SortOrder = category.SortOrder
			};
			return this.datastore.CreateCategory(c).ToDto();
		}

		public void DeleteById(String id) {
			throw new NotImplementedException();
		}

		public void DeleteWithSubElementsById(String Id) {
			throw new NotImplementedException();
		}

		public ICategoriesAndForumsDto ReadAllWithForums() {
			Tuple<IEnumerable<Dbos.Category>, IEnumerable<Dbos.Forum>> all = this.datastore.ReadAllCategoriesAndForums();

			return new Dtos.CategoriesAndForums {
				Categories = all.Item1.Select(c => c.ToDto()),
				Forums = all.Item2.Select(f => f.ToDto())
			};
		}

		//public IEnumerable<IForumDto> ReadByCategoryId(String categoryId) {
		//	throw new NotImplementedException();
		//}

		public ICategoryDto ReadById(String id) {
			ObjectId categoryId;
			if (!ObjectId.TryParse(id, out categoryId)) {
				throw new ArgumentException(nameof(id));
			}
			return this.datastore.ReadCategoryById(categoryId).ToDto();
		}

		//public IEnumerable<IForumDto> ReadByPath(IEnumerable<String> idStrings) {
		//	ObjectId[] ids = idStrings.Select(i => ObjectId.Parse(i)).ToArray();
		//	return this.forums
		//		.Find(
		//			Builders<Dbos.Forum>
		//				.Filter
		//				.In(f => f.Id, ids)
		//			)
		//		.ToList()
		//		.Select(f => f.ToDto());
		//}

		public ICategoryDto Update(Domain.Category category) {
			throw new NotImplementedException();
		}

		//IForumDto IForumDto.ReadById(String id) {
		//	ObjectId forumId = ObjectId.Parse(id);
		//	return this.forums.Find(c => c.Id == forumId).SingleOrDefault().ToDto();
		//}
	}
}
