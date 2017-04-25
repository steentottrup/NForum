using NForum.Core.Dtos;
using NForum.Datastores;
using NForum.Domain;
using NForum.Infrastructure;
using System;

namespace NForum.CQS.Commands.Forums {

	public class CreateForumCommandHandler : CommandWithStatusHandler<CreateForumCommand> {
		protected readonly IForumDatastore forums;
		protected readonly ICategoryDatastore categories;

		public CreateForumCommandHandler(ICategoryDatastore categories, IForumDatastore datastore, ITaskDatastore taskDatastore) : base(taskDatastore) {
			this.categories = categories;
			this.forums = datastore;
		}

		public override void Execute(CreateForumCommand command) {
			// Nothing special to do here, permissions have been checked and parameters validated!
			IForumDto forum = null;
			if (String.IsNullOrWhiteSpace(command.ParentForumId)) {
				ICategoryDto category = this.categories.ReadById(command.CategoryId);
				if (category == null) {
					// TODO:
					throw new ArgumentException("Given category not found");
				}
				forum = this.forums.Create(new Forum(new Category(category), command.Name, command.SortOrder, command.Description));
			}
			else {
				IForumDto parentForum = this.forums.ReadById(command.ParentForumId);
				if (parentForum == null) {
					// TODO:
					throw new ArgumentException("Given forum not found");
				}
				forum = this.forums.CreateAsForumChild(new Forum(new Forum(parentForum), command.Name, command.SortOrder, command.Description));
			}

			this.SetTaskStatus(command.TaskId, forum.Id, "Forum");
		}
	}
}
