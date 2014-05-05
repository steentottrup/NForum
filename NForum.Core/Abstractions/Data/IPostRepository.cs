using System;
using System.Collections.Generic;

namespace NForum.Core.Abstractions.Data {

	public interface IPostRepository : IRepository<Post> {
		IEnumerable<Post> Read(Topic topic, Int32 perPage, Int32 pageIndex);
		IEnumerable<Post> Read(Topic topic, Int32 perPage, Int32 pageIndex, Boolean includeQuarantined, Boolean includeDeleted);
	}
}