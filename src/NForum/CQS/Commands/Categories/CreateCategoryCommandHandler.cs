using CreativeMinds.CQS.Commands;
using NForum.Core.Dtos;
using NForum.Datastores;
using NForum.Domain;
using System;

namespace NForum.CQS.Commands.Categories {

	public class CreateCategoryCommandHandler : ICommandHandler<CreateCategoryCommand> {
		protected readonly ICategoryDatastore datastore;

		public CreateCategoryCommandHandler(ICategoryDatastore datastore) {
			this.datastore = datastore;
		}

		public void Execute(CreateCategoryCommand command) {
			// Nothing special to do here, permissions have been checked and parameters validated!
			ICategoryDto category = this.datastore.Create(new Category(command.Name, command.SortOrder, command.Description));
		}
	}
}
