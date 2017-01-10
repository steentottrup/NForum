using NForum.Core.Dtos;
using System;
using System.Collections.Generic;

namespace NForum.Datastores.EF.Dtos {

	public class CategoriesAndForums : ICategoriesAndForumsDto {
		public IEnumerable<ICategoryDto> Categories { get; set; }
		public IEnumerable<IForumDto> Forums { get; set; }
	}
}
