using NForum.Core;
using NForum.Core.Abstractions.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace NForum.Persistence.EntityFramework.Repositories {

	public class TopicRepository : RepositoryBase<Topic>, ITopicRepository {
		private readonly UnitOfWork uow;

		public TopicRepository(UnitOfWork uow)
			: base(uow) {
			this.uow = uow;
		}

		public IEnumerable<Topic> Read(Forum forum, Int32 perPage, Int32 pageIndex) {
			return this.Read(forum, perPage, pageIndex, true, true);
		}

		public IEnumerable<Topic> Read(Forum forum, Int32 perPage, Int32 pageIndex, Boolean includeQuarantined, Boolean includeDeleted) {
			if (forum == null) {
				throw new ArgumentNullException("forum");
			}
			IQueryable<Topic> topics = this.set;
			if (!includeDeleted) {
				topics = topics.Where(p => p.State != TopicState.Deleted);
			}
			if (!includeQuarantined) {
				topics = topics.Where(p => p.State != TopicState.Quarantined);
			}
			return topics
				.Where(t => t.ForumId == forum.Id)
				.Include(t => t.Author)
				// TODO: hm...?!?!?!
				.Include(t => t.Posts)
				//.Include(t => t.LatestPost)
				// TODO: Working ?!?!?
				.OrderByDescending(t => t.Posts.Any(p => p.State == PostState.None || (p.State == PostState.Deleted && includeDeleted) || (p.State == PostState.Quarantined && includeDeleted)) ? t.Posts.Where(p => p.State == PostState.None || (p.State == PostState.Deleted && includeDeleted) || (p.State == PostState.Quarantined && includeDeleted)).OrderByDescending(po => po.Created).First().Created : t.Created)
				.Skip(pageIndex * perPage)
				.Take(perPage)
				.ToList();
		}

		public Topic BySubject(String subject) {
			return this.set.FirstOrDefault(t => t.Subject == subject);
		}

		public Topic GetLatest(IEnumerable<Forum> forums) {
			Int32[] ids = forums.Select(f => f.Id).ToArray();
			return this.set
				.Where(t => ids.Contains(t.ForumId))
				.Where(t => t.State != TopicState.Deleted && t.State != TopicState.Quarantined)
				.OrderByDescending(t => t.Created)
				.FirstOrDefault();
		}
	}
}