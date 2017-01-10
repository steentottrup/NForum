using CreativeMinds.CQS.Commands;
using System;
using System.Threading.Tasks;

namespace NForum.CQS.Commands.Categories {

	public class UpdateCategoryCommandHandler : ICommandHandler<UpdateCategoryCommand> {
		public void Execute(UpdateCategoryCommand command) {
			throw new NotImplementedException();
		}

		public Task ExecuteAsync(UpdateCategoryCommand command) {
			throw new NotImplementedException();
		}
	}
}
