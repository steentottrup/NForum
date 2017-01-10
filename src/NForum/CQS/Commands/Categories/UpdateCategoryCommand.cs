using CreativeMinds.CQS.Commands;
using System;

namespace NForum.CQS.Commands.Categories {

	public class UpdateCategoryCommand : ICommand {
		public String Id { get; set; }
		public String Name { get; set; }
		public String Description { get; set; }
		public Int32 SortOrder { get; set; }
	}
}
