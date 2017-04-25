using CreativeMinds.CQS.Queries;
using NForum.Core.Dtos;
using NForum.Datastores;
using System;

namespace NForum.CQS.Queries.Categories {

	public class ReadCategoryQueryHandler : IQueryHandler<ReadCategoryQuery, ICategoryDto> {
		protected readonly ICategoryDatastore categoryDatastore;

		public ReadCategoryQueryHandler(ICategoryDatastore categoryDatastore) {
			this.categoryDatastore = categoryDatastore;
		}

		public ICategoryDto Handle(ReadCategoryQuery query) {
			return this.categoryDatastore.ReadById(query.CategoryId);
		}
	}
}
