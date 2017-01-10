using CreativeMinds.CQS.Decorators;
using System;

namespace NForum.CQS.Commands.Categories {

	[Validate]
	[CheckPermissions]
	public class CreateCategoryCommand : CommandWithStatus {
		public String Name { get; set; }
		public String Description { get; set; }
		public Int32 SortOrder { get; set; }
	}
}
