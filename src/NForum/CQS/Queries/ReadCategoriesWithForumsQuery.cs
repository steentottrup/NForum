using CreativeMinds.CQS.Decorators;
using CreativeMinds.CQS.Queries;
using System;

namespace NForum.CQS.Queries {

	[CheckPermissions]
	public class ReadCategoriesWithForumsQuery : IQuery<CategoriesAndForums> {
	}
}
