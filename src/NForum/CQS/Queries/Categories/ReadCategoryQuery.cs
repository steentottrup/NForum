using CreativeMinds.CQS.Queries;
using NForum.Core.Dtos;
using System;

namespace NForum.CQS.Queries.Categories {

	public class ReadCategoryQuery : IQuery<ICategoryDto> {
		public String CategoryId { get; set; }
	}
}
