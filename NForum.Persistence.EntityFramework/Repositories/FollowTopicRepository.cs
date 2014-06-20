using NForum.Core;
using NForum.Core.Abstractions.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace NForum.Persistence.EntityFramework.Repositories {

	public class FollowTopicRepository : RepositoryBase<FollowTopic>, IFollowTopicRepository {

		public FollowTopicRepository(UnitOfWork uow)
			: base(uow) {
		}

		public IEnumerable<FollowTopic> ByTopic(Topic topic) {
			return this.set.Include(ft => ft.User)
						.Where(ft => ft.TopicId == topic.Id)
						.ToList();
		}

		public FollowTopic ByUserAndTopic(Topic topic, User user) {
			return this.set.FirstOrDefault(ft => ft.TopicId == topic.Id && ft.UserId == user.Id);
		}
	}
}