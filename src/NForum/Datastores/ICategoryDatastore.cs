using NForum.Core.Dtos;
using NForum.Domain;
using System;
using System.Collections.Generic;

namespace NForum.Datastores {

	public interface ICategoryDatastore {
		//Task<ICategoryDto> CreateAsync(Category category);
		ICategoryDto Create(Category category);
		ICategoriesAndForumsDto ReadAllWithForums();
	}
}
