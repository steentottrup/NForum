using CreativeMinds.CQS.Commands;
using NForum.Core.Dtos;
using NForum.Datastores;
using NForum.Domain;
using NForum.Infrastructure;
using System;

namespace NForum.CQS.Commands.Categories {

	public class CreateCategoryCommandHandler : CommandWithStatusHandler<CreateCategoryCommand> {
		protected readonly ICategoryDatastore datastore;

		public CreateCategoryCommandHandler(ICategoryDatastore datastore, ITaskDatastore taskDatastore) : base(taskDatastore) {
			this.datastore = datastore;
		}

		public override void Execute(CreateCategoryCommand command) {
			// Nothing special to do here, permissions have been checked and parameters validated!
			ICategoryDto category = this.datastore.Create(new Category(command.Name, command.SortOrder, command.Description));
			this.SetTaskStatus(command.TaskId, category.Id, "Category");
		}
	}
}
