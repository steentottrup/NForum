using CreativeMinds.CQS.Commands;
using System;
using System.Threading.Tasks;

namespace NForum.CQS.Commands.Forums {

	public class DeleteForumCommandHandler : ICommandHandler<DeleteForumCommand> {
		public void Execute(DeleteForumCommand command) {
			throw new NotImplementedException();
		}

		public Task ExecuteAsync(DeleteForumCommand command) {
			throw new NotImplementedException();
		}
	}
}
