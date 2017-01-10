using CreativeMinds.CQS.Commands;
using System;
using System.Threading.Tasks;

namespace NForum.CQS.Commands.Categories {

	public class DeleteCategoryCommandHandler : ICommandHandler<DeleteCategoryCommand> {
		public void Execute(DeleteCategoryCommand command) {
			throw new NotImplementedException();
		}

		public Task ExecuteAsync(DeleteCategoryCommand command) {
			throw new NotImplementedException();
		}
	}
}
