using System;
using System.Collections.Generic;

namespace NForum.Core.Abstractions.Services {

	public interface ICategoryService {
		Category Create(String name, String description, Int32 sortOrder);

		Category Read(Int32 id);
		Category Read(String name);
		IEnumerable<Category> Read();

		Category Update(Category category);

		void Delete(Category category);
	}
}