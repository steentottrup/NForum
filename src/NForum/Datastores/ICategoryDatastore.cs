using NForum.Core.Dtos;
using NForum.Domain;
using NForum.Domain.Abstractions;
using System;
using System.Collections.Generic;

namespace NForum.Datastores {

	public interface ICategoryDatastore {
		ICategoryDto Create(Category category);
		ICategoryDto ReadById(String id);
		ICategoriesAndForumsDto ReadAllWithForums();
		ICategoryDto Update(Category category);
		void DeleteById(String id);
		void DeleteWithSubElementsById(String Id);
	}
}
