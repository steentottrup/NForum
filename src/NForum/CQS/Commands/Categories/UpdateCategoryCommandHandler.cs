using CreativeMinds.CQS.Commands;
using NForum.Core.Dtos;
using NForum.Datastores;
using NForum.Domain;
using NForum.Infrastructure;
using System;

namespace NForum.CQS.Commands.Categories {

	public class UpdateCategoryCommandHandler : CommandWithStatusHandler<UpdateCategoryCommand> {
		protected readonly ICategoryDatastore categories;

		public UpdateCategoryCommandHandler(ICategoryDatastore categories, ITaskDatastore taskDatastore) : base(taskDatastore) {
			this.categories = categories;
		}

		public override void Execute(UpdateCategoryCommand command) {
			// Permissions have been checked and parameters validated!
			ICategoryDto dto = this.categories.ReadById(command.Id);
			if (dto == null) {
				throw new CategoryNotFoundException(command.Id);
			}

			Category c = new Category(dto);
			c.Name = command.Name;
			c.SortOrder = command.SortOrder;
			c.Description = command.Description;
			c.ClearAndAddProperties(c.Properties);

			this.categories.Update(c);

			this.SetTaskStatus(command.TaskId, command.Id, "Category");
		}
	}
}
