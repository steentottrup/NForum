using CreativeMinds.CQS.Commands;
using NForum.Core.Dtos;
using NForum.Datastores;
using NForum.Domain;
using System;

namespace NForum.CQS.Commands.Categories {

	public class UpdateCategoryCommandHandler : ICommandHandler<UpdateCategoryCommand> {
		protected readonly ICategoryDatastore categories;

		public UpdateCategoryCommandHandler(ICategoryDatastore categories) {
			this.categories = categories;
		}

		public void Execute(UpdateCategoryCommand command) {
			// Permissions have been checked and parameters validated!
			ICategoryDto dto = this.categories.ReadById(command.Id);
			if (dto == null) {
				// TODO:
				throw new ArgumentException("Category does not exist");
			}

			Category c = new Category(dto);
			c.Name = command.Name;
			c.SortOrder = command.SortOrder;
			c.Description = command.Description;
			c.ClearAndAddProperties(c.Properties);

			this.categories.Update(c);
		}
	}
}
