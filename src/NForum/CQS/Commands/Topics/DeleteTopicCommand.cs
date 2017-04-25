using CreativeMinds.CQS.Commands;
using System;

namespace NForum.CQS.Commands.Topics {

	public class DeleteTopicCommand : CommandWithStatus {
		public String Id { get; set; }
	}
}
