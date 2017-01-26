using CreativeMinds.CQS.Queries;
using NForum.Core.Dtos;
using NForum.Datastores;
using System;
using System.Collections.Generic;

namespace NForum.CQS.Queries.Forums {

	public class ReadBreadcrumbForForumQueryHandler : IQueryHandler<ReadBreadcrumbForForumQuery, ReadBreadcrumbForForum> {
		protected readonly IForumDatastore forumDatastore;
		protected readonly ICategoryDatastore categoryDatastore;

		public ReadBreadcrumbForForumQueryHandler(ICategoryDatastore categoryDatastore, IForumDatastore forumDatastore) {
			this.categoryDatastore = categoryDatastore;
			this.forumDatastore = forumDatastore;
		}

		public ReadBreadcrumbForForum Handle(ReadBreadcrumbForForumQuery query) {
			IForumDto forum = this.forumDatastore.ReadById(query.ForumId);

			ICategoryDto category = this.categoryDatastore.ReadById(forum.Category.Id);

			IEnumerable<IForumDto> forums = this.forumDatastore.ReadByPath(forum.Path);

			return new ReadBreadcrumbForForum { 
				Category = category,
				Forums = forums
			};
		}
	}
}
