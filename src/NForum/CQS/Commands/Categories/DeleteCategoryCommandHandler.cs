using CreativeMinds.CQS.Commands;
using NForum.Datastores;
using NForum.Infrastructure;
using System;

namespace NForum.CQS.Commands.Categories {

	public class DeleteCategoryCommandHandler : CommandWithStatusHandler<DeleteCategoryCommand> {
		protected readonly ICategoryDatastore categories;
		protected readonly IForumDatastore forums;

		public DeleteCategoryCommandHandler(ICategoryDatastore categories, IForumDatastore forums, ITaskDatastore taskDatastore) : base(taskDatastore) {
			this.categories = categories;
			this.forums = forums;
		}

		public override void Execute(DeleteCategoryCommand command) {
			// Permissions have been checked and parameters validated!
			if (!command.DeleteChildren) {
				this.categories.DeleteById(command.Id);
			}
			else {
				this.categories.DeleteWithSubElementsById(command.Id);
			}

			this.SetTaskStatus(command.TaskId, "", "Category");
		}
	}
}
