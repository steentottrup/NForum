using CreativeMinds.CQS.Commands;
using System;
using System.Threading.Tasks;

namespace NForum.CQS.Commands.Forums {

	public class UpdateForumCommandHandler : ICommandHandler<UpdateForumCommand> {
		public void Execute(UpdateForumCommand command) {
			throw new NotImplementedException();
		}

		public Task ExecuteAsync(UpdateForumCommand command) {
			throw new NotImplementedException();
		}
	}
}
