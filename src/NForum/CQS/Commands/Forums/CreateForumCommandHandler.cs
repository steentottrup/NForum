using CreativeMinds.CQS.Commands;
using System;
using System.Threading.Tasks;

namespace NForum.CQS.Commands.Forums {

	public class CreateForumCommandHandler : ICommandHandler<CreateForumCommand> {
		public void Execute(CreateForumCommand command) {
			throw new NotImplementedException();
		}

		public Task ExecuteAsync(CreateForumCommand command) {
			throw new NotImplementedException();
		}
	}
}
