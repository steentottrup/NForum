using NForum.Core;
using NForum.Core.Abstractions.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace NForum.Persistence.EntityFramework.Repositories {

	public class TopicTrackerRepository : RepositoryBase<TopicTracker>, ITopicTrackerRepository {

		public TopicTrackerRepository(UnitOfWork uow)
			: base(uow) {
		}

		public TopicTracker ByUserAndTopic(User user, Topic topic) {
			return this.set
					.FirstOrDefault(ft => ft.UserId == user.Id && ft.TopicId == topic.Id);
		}

		public IEnumerable<TopicTracker> ByUser(User user) {
			return this.set
					.Where(ft => ft.UserId == user.Id)
					.ToList();
		}

		public IEnumerable<TopicTracker> ByUserAndForum(User user, Forum forum) {
			return this.set
					.Include(tt => tt.Topic)
					.Where(tt => tt.UserId == user.Id && tt.Topic.ForumId == forum.Id)
					.ToList();
		}
	}
}