using CreativeMinds.CQS.Commands;
using System;
using System.Threading.Tasks;

namespace NForum.CQS.Commands.Forums {

	public class MoveForumCommandHandler : ICommandHandler<MoveForumCommand> {
		public void Execute(MoveForumCommand command) {
			throw new NotImplementedException();
		}

		public Task ExecuteAsync(MoveForumCommand command) {
			throw new NotImplementedException();
		}
	}
}
