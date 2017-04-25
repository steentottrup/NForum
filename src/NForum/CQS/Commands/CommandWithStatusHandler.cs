using CreativeMinds.CQS.Commands;
using NForum.Infrastructure;
using System;

namespace NForum.CQS.Commands {

	public abstract class CommandWithStatusHandler<TCommand> : ICommandHandler<TCommand> where TCommand : CommandWithStatus {
		protected readonly ITaskDatastore taskDatastore;

		public CommandWithStatusHandler(ITaskDatastore taskDatastore) {
			this.taskDatastore = taskDatastore;
		}

		protected virtual void SetTaskStatus(Guid id, String elementId, String type /* status ???*/) {
			this.taskDatastore.SetTaskStatus(id, elementId, type);
		}

		public abstract void Execute(TCommand command);
	}
}
