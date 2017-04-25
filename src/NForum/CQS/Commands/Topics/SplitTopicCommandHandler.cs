using CreativeMinds.CQS.Commands;
using NForum.Infrastructure;
using System;

namespace NForum.CQS.Commands.Topics {

	public class SplitTopicCommandHandler : CommandWithStatusHandler<SplitTopicCommand> {

		public SplitTopicCommandHandler(ITaskDatastore taskDatastore) : base(taskDatastore) {

		}

		public override void Execute(SplitTopicCommand command) {
			throw new NotImplementedException();
		}
	}
}
