using CreativeMinds.CQS.Commands;
using System;

namespace NForum.CQS.Commands.Categories {

	public class DeleteCategoryCommand : CommandWithStatus {
		public String Id { get; set; }
		public Boolean DeleteChildren { get; set; }
	}
}
