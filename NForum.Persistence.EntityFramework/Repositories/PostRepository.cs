using NForum.Core;
using NForum.Core.Abstractions.Data;
using System;
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
	}
}