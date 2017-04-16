using CreativeMinds.CQS.Commands;
using NForum.Datastores;
using NForum.Infrastructure;
using System;

namespace NForum.CQS.Commands.Categories {

	public class DeleteCategoryCommandHandler : ICommandHandler<DeleteCategoryCommand> {
		protected readonly ICategoryDatastore categories;
		protected readonly IForumDatastore forums;

		public DeleteCategoryCommandHandler(ICategoryDatastore categories, IForumDatastore forums) {
			this.categories = categories;
			this.forums = forums;
		}

		public void Execute(DeleteCategoryCommand command) {
			// Permissions have been checked and parameters validated!
			if (!command.DeleteChildren) {
				this.categories.DeleteById(command.Id);
			}
			else {
				this.categories.DeleteWithSubElementsById(command.Id);
			}
		}
	}
}
