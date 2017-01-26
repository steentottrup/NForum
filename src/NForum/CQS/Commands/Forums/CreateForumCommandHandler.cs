using CreativeMinds.CQS.Commands;
using NForum.Core.Dtos;
using NForum.Datastores;
using NForum.Domain;
using System;

namespace NForum.CQS.Commands.Forums {

	public class CreateForumCommandHandler : ICommandHandler<CreateForumCommand> {
		protected readonly IForumDatastore forums;
		protected readonly ICategoryDatastore categories;

		public CreateForumCommandHandler(ICategoryDatastore categories, IForumDatastore datastore) {
			this.categories = categories;
			this.forums = datastore;
		}

		public void Execute(CreateForumCommand command) {
			// Nothing special to do here, permissions have been checked and parameters validated!
			if (String.IsNullOrWhiteSpace(command.ParentForumId)) {
				ICategoryDto category = this.categories.ReadById(command.CategoryId);
				if (category == null) {
					// TODO:
					throw new ArgumentException("Given category not found");
				}
				this.forums.Create(new Forum(new Category(category), command.Name, command.SortOrder, command.Description));
			}
			else {
				IForumDto parentForum = this.forums.ReadById(command.ParentForumId);
				if (parentForum == null) {
					// TODO:
					throw new ArgumentException("Given forum not found");
				}
				this.forums.Create(new Forum(new Forum(parentForum), command.Name, command.SortOrder, command.Description));
			}
		}
	}
}
