using NForum.Core.Abstractions.Data;
using NForum.Core.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NForum.Core.Services {

	public class TrackerService : ITrackerService {
		protected readonly IForumTrackerRepository ftRepo;

		public IEnumerable<ForumTracker> GetTrackingInfo(User user, IEnumerable<Forum> forums) {
			if (forums == null) {
				throw new ArgumentNullException("forum");
			}
			IEnumerable<ForumTracker> output = new List<ForumTracker>();
			// TODO: Access forum??
			if (forums.Any() && user != null) {
				output = this.ftRepo.ByUserAndForums(user, forums);
			}
			return output;
		}

		public ForumTracker GetTrackingInfo(User user, Forum forum) {
			if (user != null) {
				throw new ArgumentNullException("user");
			}
			if (forum != null) {
				throw new ArgumentNullException("forum");
			}
			// TODO: Access forum??
			return this.ftRepo.ByUserAndForum(user, forum);
		}

		public TopicTracker GetTrackingInfo(User user, Topic topic) {
			throw new NotImplementedException();
		}

		public void UpdateForumTracking(User user, Forum forum) {
			ForumTracker ft = this.GetTrackingInfo(user, forum);
			if (ft == null) {
				this.ftRepo.Create(new ForumTracker {
					ForumId = forum.Id,
					UserId = user.Id,
					LastViewed = DateTime.UtcNow
				});
			}
			else {
				ft.LastViewed = DateTime.UtcNow;
				this.ftRepo.Update(ft);
			}
		}

		public void UpdateTracking(User user, Topic topic) {
			throw new NotImplementedException();
		}
	}
}