using NForum.Core.Abstractions.Data;
using NForum.Core.Abstractions.Services;
using System;

namespace NForum.Core.Services {

	public class TrackerService : ITrackerService {
		protected readonly IForumTrackerRepository ftRepo;

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