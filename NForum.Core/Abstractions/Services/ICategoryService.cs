using System;
using System.Collections.Generic;

namespace NForum.Core.Abstractions.Services {

	public interface ICategoryService {
		Category Create(Board board, String name, String description, Int32 sortOrder);

		Category Read(Int32 id);
		Category Read(String name);
		IEnumerable<Category> Read(Board board);

		Category Update(Category category);

		void Delete(Category category);
	}
}