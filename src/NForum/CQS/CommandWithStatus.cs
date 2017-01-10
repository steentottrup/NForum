using CreativeMinds.CQS.Commands;
using System;

namespace NForum.CQS {

	public class CommandWithStatus : ICommand {

		public CommandWithStatus() {
			this.TaskId = Guid.NewGuid();
		}

		public Guid TaskId { get; private set; }
	}
}
