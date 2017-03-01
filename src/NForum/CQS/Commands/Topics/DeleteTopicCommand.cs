using CreativeMinds.CQS.Commands;
using System;

namespace NForum.CQS.Commands.Topics {

	public class DeleteTopicCommand : ICommand {
		public String Id { get; set; }
	}
}
