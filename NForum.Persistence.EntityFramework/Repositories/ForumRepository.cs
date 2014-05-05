using NForum.Core;
using NForum.Core.Abstractions.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace NForum.Persistence.EntityFramework.Repositories {

	public class ForumRepository : RepositoryBase<Forum>, IForumRepository {
		private readonly UnitOfWork uow;

		public ForumRepository(UnitOfWork uow)
			: base(uow) {
			this.uow = uow;
		}

		private IQueryable<Forum> GetExpandedForum() {
			return this.uow.Set<Forum>()
				.Include(f => f.Category)
				.Include(f => f.LatestPost)
				.Include(f => f.LatestTopic)
				.Include(f => f.ParentForum)
				.Include(f => f.LatestPost.Author)
				.Include(f => f.LatestTopic.Author)
				// TODO:
				//.Include(f=>f.SubForums)
				;
		}

		public Forum ByName(String name) {
			return this.GetExpandedForum().FirstOrDefault(f => f.Name == name);
		}

		public IEnumerable<Forum> ByCategory(Category category) {
			return this.GetExpandedForum().Where(f => f.CategoryId == category.Id).ToList();
		}

		public Forum ByTopic(Topic topic) {
			return this.GetExpandedForum().FirstOrDefault(f => f.Id == topic.ForumId);
		}

		public Forum ByPost(Post post) {
			return this.GetExpandedForum().Include(f => f.Topics).FirstOrDefault(f => f.Topics.Any(t => t.Id == post.TopicId) == true);
		}

		public Forum ByForum(Forum forum) {
			if (!forum.ParentForumId.HasValue) {
				// TODO:
				throw new ApplicationException("no parent forum");
			}
			return this.GetExpandedForum().FirstOrDefault(f => f.Id == forum.ParentForumId);
		}

		public IEnumerable<Forum> ByBoard(Board board) {
			return this.GetExpandedForum().Where(b => b.Category.BoardId == board.Id);
		}
	}
}