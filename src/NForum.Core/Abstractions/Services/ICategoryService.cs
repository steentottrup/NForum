using System;
using System.Collections.Generic;

namespace NForum.Core.Abstractions.Services {

	public interface ICategoryService {
		Category Create(String name, Int32 sortOrder, String description);
		Category FindById(String categoryId);
		IEnumerable<Category> FindAll();
		Category Update(String categoryId, String name, Int32 sortOrder, String description);
		Boolean Delete(String categoryId);
	}
}
