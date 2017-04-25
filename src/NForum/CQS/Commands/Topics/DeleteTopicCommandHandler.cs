using CreativeMinds.CQS.Commands;
using NForum.Infrastructure;
using System;

namespace NForum.CQS.Commands.Topics {

	public class DeleteTopicCommandHandler : CommandWithStatusHandler<DeleteTopicCommand> {

		public DeleteTopicCommandHandler(ITaskDatastore taskDatastore) : base(taskDatastore) {

		}

		public override void Execute(DeleteTopicCommand command) {
			throw new NotImplementedException();
		}
	}
}
