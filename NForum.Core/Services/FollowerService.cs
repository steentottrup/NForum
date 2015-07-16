using NForum.Core.Abstractions;
using NForum.Core.Abstractions.Data;
using NForum.Core.Abstractions.Providers;
using NForum.Core.Abstractions.Services;
using System;
using System.Collections.Generic;

namespace NForum.Core.Services {

	public class FollowerService : IFollowerService {
		protected readonly IFollowForumRepository forumRepo;
		protected readonly IFollowTopicRepository topicRepo;
		protected readonly IUserProvider userProvider;
		protected readonly ILogger logger;
		protected readonly IPermissionService permService;

		public FollowerService(IFollowForumRepository forumRepo,
								IFollowTopicRepository topicRepo,
								IPermissionService permService,
								IUserProvider userProvider,
								ILogger logger) {

			this.forumRepo = forumRepo;
			this.topicRepo = topicRepo;
			this.logger = logger;
			this.userProvider = userProvider;
			this.permService = permService;
		}

		public IEnumerable<FollowForum> GetFollowers(Forum forum) {
			//User user = this.userProvider.CurrentUser
			// TODO: Access?? Or not!
			return this.forumRepo.ByForum(forum);
		}

		public IEnumerable<FollowTopic> GetFollowers(Topic topic) {
			// TODO: Access?? Or not!
			return this.topicRepo.ByTopic(topic);
		}

		public void Follow(Forum forum) {
			if (forum == null) {
				throw new ArgumentNullException("forum");
			}
			User user = this.userProvider.CurrentUser;
			if (user == null) {
				throw new NoAuthenticatedUserFoundException();
			}
			// TODO: Permissions??
			if (this.forumRepo.ByUserAndForum(forum, user) == null) {
				this.forumRepo.Create(new FollowForum {
					ForumId = forum.Id,
					UserId = user.Id
				});
			}
		}

		public void Follow(Topic topic) {
			if (topic == null) {
				throw new ArgumentNullException("topic");
			}
			User user = this.userProvider.CurrentUser;
			if (user == null) {
				throw new NoAuthenticatedUserFoundException();
			}
			if (this.topicRepo.ByUserAndTopic(topic, user) == null) {
				this.topicRepo.Create(new FollowTopic {
					TopicId = topic.Id,
					UserId = user.Id
				});
			}
		}

		public void UnFollow(Forum forum) {
			if (forum == null) {
				throw new ArgumentNullException("forum");
			}
			User user = this.userProvider.CurrentUser;
			if (user == null) {
				throw new NoAuthenticatedUserFoundException();
			}
			FollowForum ff = this.forumRepo.ByUserAndForum(forum, user);
			if (ff != null) {
				this.forumRepo.Delete(ff);
			}
		}

		public void UnFollow(Topic topic) {
			if (topic == null) {
				throw new ArgumentNullException("topic");
			}
			User user = this.userProvider.CurrentUser;
			if (user == null) {
				throw new NoAuthenticatedUserFoundException();
			}
			FollowTopic ft = this.topicRepo.ByUserAndTopic(topic, user);
			if (ft != null) {
				this.topicRepo.Delete(ft);
			}
		}
	}
}