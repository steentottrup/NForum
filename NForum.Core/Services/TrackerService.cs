using NForum.Core.Abstractions.Data;
using NForum.Core.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NForum.Core.Services {

	public class TrackerService : ITrackerService {
		protected readonly IForumTrackerRepository ftRepo;
		protected readonly ITopicTrackerRepository ttRepo;
		protected readonly IPermissionService permService;

		public TrackerService(IForumTrackerRepository ftRepo, ITopicTrackerRepository ttRepo, IPermissionService permService) {
			this.ftRepo = ftRepo;
			this.ttRepo = ttRepo;
			this.permService = permService;

		}

		public IEnumerable<ForumTracker> GetTrackingInfo(User user, IEnumerable<Forum> forums) {
			if (forums == null) {
				throw new ArgumentNullException("forum");
			}
			IEnumerable<ForumTracker> output = new List<ForumTracker>();
			// Get the forums the user actually can access!
			forums = this.permService.GetAccessible(user, forums);
			if (forums.Any() && user != null) {
				output = this.ftRepo.ByUserAndForums(user, forums);
			}
			return output;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="user"></param>
		/// <param name="forum"></param>
		/// <returns>null, if the user does can not access the forum</returns>
		public ForumTracker GetTrackingInfo(User user, Forum forum) {
			if (user != null) {
				throw new ArgumentNullException("user");
			}
			if (forum != null) {
				throw new ArgumentNullException("forum");
			}
			// Can the user access the forum?
			if (this.permService.HasAccess(user, forum)) {
				return null;
			}
			return this.ftRepo.ByUserAndForum(user, forum);
		}

		public TopicTracker GetTrackingInfo(User user, Topic topic) {
			if (user != null) {
				throw new ArgumentNullException("user");
			}
			if (topic != null) {
				throw new ArgumentNullException("topic");
			}
			// Can the user access the topic?
			if (this.permService.HasAccess(user, topic.Forum)) {
				return null;
			}
			return this.ttRepo.ByUserAndTopic(user, topic);
		}

		public void UpdateForumTracking(User user, Forum forum) {
			if (this.permService.HasAccess(user, forum)) {
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
		}

		public void UpdateTracking(User user, Topic topic) {
			if (this.permService.HasAccess(user, topic.Forum)) {
				TopicTracker tt = this.GetTrackingInfo(user, topic);
				if (tt == null) {
					this.ttRepo.Create(new TopicTracker {
						TopicId = topic.Id,
						UserId = user.Id,
						LastViewed = DateTime.UtcNow
					});
				}
				else {
					tt.LastViewed = DateTime.UtcNow;
					this.ttRepo.Update(tt);
				}
			}
		}
	}
}