using System;
using System.Collections.Generic;

namespace NForum.Core.Dtos {

	public interface ICategoriesAndForumsDto {
		IEnumerable<ICategoryDto> Categories { get; }
		IEnumerable<IForumDto> Forums { get; }
	}
}
