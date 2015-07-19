using NForum.Core;
using NForum.Core.Abstractions.Data;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;

namespace NForum.Persistence.EntityFramework.Repositories {

	public class PostRepository : RepositoryBase<Post>, IPostRepository {
		private readonly UnitOfWork uow;

		public PostRepository(UnitOfWork uow)
			: base(uow) {
			this.uow = uow;
		}

		/// <summary>
		/// Method for returning all posts of a topic for the given page.
		/// </summary>
		/// <param name="topic">Parent topic.</param>
		/// <param name="perPage">Posts to get per page.</param>
		/// <param name="pageIndex">Page index to get, 0 indexed.</param>
		/// <returns></returns>
		public IEnumerable<Post> Read(Topic topic, Int32 perPage, Int32 pageIndex) {
			return this.Read(topic, perPage, pageIndex, true, true);
		}

		/// <summary>
		/// Method for returning all posts matching the parameters.
		/// </summary>
		/// <param name="topic">Parent topic.</param>
		/// <param name="perPage">Posts to get per page.</param>
		/// <param name="pageIndex">Page index to get, 0 indexed.</param>
		/// <param name="includeQuarantined">Should quarantined posts be included?</param>
		/// <param name="includeDeleted">Should deleted posts be included?</param>
		/// <returns></returns>
		public IEnumerable<Post> Read(Topic topic, Int32 perPage, Int32 pageIndex, Boolean includeQuarantined, Boolean includeDeleted) {
			if (topic == null) {
				throw new ArgumentNullException("topic");
			}
			IQueryable<Post> posts = this.set;
			if (!includeDeleted) {
				posts = posts.Where(p => p.State != PostState.Deleted);
			}
			if (!includeQuarantined) {
				posts = posts.Where(p => p.State != PostState.Quarantined);
			}
			return posts
				.Where(p => p.TopicId == topic.Id)
				.OrderBy(p => p.Created)
				.Skip(pageIndex * perPage)
				.Take(perPage)
				.ToList();
		}

		public Post GetLatest(IEnumerable<Forum> forums) {
			Int32[] ids = forums.Select(f => f.Id).ToArray();
			return this.set
				.Include(p => p.Topic)
				.Where(p => ids.Contains(p.ForumId))
				.Where(p => p.State != PostState.Deleted && p.State != PostState.Quarantined)
				.Where(p => p.Topic.State != TopicState.Quarantined && p.Topic.State != TopicState.Deleted)
				.OrderByDescending(p => p.Created)
				.FirstOrDefault();
		}

		public Post GetLatest(Topic topic) {
			return this.set
				.Where(p => p.TopicId == topic.Id)
				.Where(p => p.State != PostState.Deleted && p.State != PostState.Quarantined)
				.OrderByDescending(p => p.Created)
				.FirstOrDefault();
		}
	}
}