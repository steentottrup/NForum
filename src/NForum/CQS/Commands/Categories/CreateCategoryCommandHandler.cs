using CreativeMinds.CQS.Commands;
using NForum.Core.Dtos;
using NForum.Datastores;
using NForum.Domain;
using System;
using System.Threading.Tasks;

namespace NForum.CQS.Commands.Categories {

	public class CreateCategoryCommandHandler : ICommandHandler<CreateCategoryCommand> {
		private readonly ICategoryDatastore datastore;

		public CreateCategoryCommandHandler(ICategoryDatastore datastore) {
			this.datastore = datastore;
		}

		public void Execute(CreateCategoryCommand command) {
			ICategoryDto category = this.datastore.Create(new Category(command.Name, command.SortOrder, command.Description));
		}

		//public async Task ExecuteAsync(CreateCategoryCommand command) {
		//	ICategoryDto category = await this.datastore.Create(new Category(command.Name, command.SortOrder, command.Description));
		//	// TODO: Task!!!
		//}
	}
}
