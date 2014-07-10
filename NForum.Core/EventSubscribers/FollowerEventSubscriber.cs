using NForum.Core.Abstractions;
using NForum.Core.Abstractions.Data;
using NForum.Core.Abstractions.Events;
using NForum.Core.Abstractions.Providers;
using NForum.Core.Abstractions.Services;
using NForum.Core.Events;
using NForum.Core.Events.Payloads;
using NForum.Core.Providers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace NForum.Core.EventSubscribers {

	public class FollowerEventSubscriber : IEventSubscriber<PostCreated>,
											IEventSubscriber<TopicCreated>,
											IEventSubscriber<TopicStateUpdated>,
											IEventSubscriber<PostStateUpdated> {

		protected readonly IFollowerService followerService;
		protected readonly IOutDataService mailService;
		protected readonly ILogger logger;
		protected readonly IUserProvider userProvider;
		protected readonly IPostRepository postRepo;
		protected readonly ITopicRepository topicRepo;
		protected readonly ITextProvider textProvider;
		protected readonly IForumConfigurationService confService;

		public FollowerEventSubscriber(IFollowerService followerService,
										IOutDataService mailService,
										ITopicRepository topicRepo,
										IPostRepository postRepo,
										IUserProvider userProvider,
										ITextProvider textProvider,
										IForumConfigurationService confService,
										ILogger logger) {

			this.followerService = followerService;
			this.mailService = mailService;
			this.textProvider = textProvider;
			this.logger = logger;
			this.postRepo = postRepo;
			this.userProvider = userProvider;
			this.topicRepo = topicRepo;
			this.confService = confService;
		}

		public void Handle(Object payload, IState request) {
			if (payload is PostCreated) {
				this.Handle((PostCreated)payload, request);
			}
			else if (payload is PostStateUpdated) {
				this.Handle((PostStateUpdated)payload, request);
			}
			else if (payload is TopicCreated) {
				this.Handle((TopicCreated)payload, request);
			}
			else if (payload is TopicStateUpdated) {
				this.Handle((TopicStateUpdated)payload, request);
			}
			else {
				throw new ArgumentException("unknown payload");
			}
		}

		public void Handle(PostCreated payload, IState request) {
			Post post = this.postRepo.Read(payload.Post.Id);
			if (post.IsVisible()) {
				this.InformTopicFollowers(post.Topic, post);
			}
		}

		public void Handle(TopicCreated payload, IState request) {
			Topic topic = this.topicRepo.Read(payload.Topic.Id);
			if (topic.IsVisible()) {
				this.InformForumFollowers(topic.Forum, topic);
			}
		}

		public void Handle(TopicStateUpdated payload, IState request) {
			Topic topic = this.topicRepo.Read(payload.UpdatedTopic.Id);
			if (topic.GonePublic(payload.OriginalTopic)) {
				this.InformForumFollowers(topic.Forum, topic);
			}
		}

		public void Handle(PostStateUpdated payload, IState request) {
			Post post = this.postRepo.Read(payload.UpdatedPost.Id);
			if (post.GonePublic(payload.OriginalPost)) {
				this.InformTopicFollowers(post.Topic, post);
			}
		}

		private void InformForumFollowers(Forum forum, Topic topic) {
			IEnumerable<FollowForum> followers = this.followerService.GetFollowers(forum);
			User user = this.userProvider.CurrentUser;

			String senderName = this.confService.Read().SenderName();
			String senderEmailAddress = this.confService.Read().SenderEmailAddress();
			MailType type = MailType.Weekly;
			if (this.confService.Read().CustomPropertyExists("DefaultFollowerFrequency")) {
				Enum.TryParse<MailType>(this.confService.Read().GetCustomPropertyString("DefaultFollowerFrequency"), out type);
			}

			Dictionary<String, IRegionalSettingsProvider> regionalCache = new Dictionary<String, IRegionalSettingsProvider>();
			foreach (FollowForum ff in followers.Where(ff => ff.UserId != user.Id)) {
				User follower = ff.User;

				if (!regionalCache.ContainsKey(follower.Culture + follower.TimeZone)) {
					// We're going to need a "hard-coded" regional settings, using the regular provider will give us the settings of the current user!
					regionalCache.Add(follower.Culture + follower.TimeZone, new RegionalSettingsProvider(new CultureInfo(follower.Culture), TimeZoneInfo.FindSystemTimeZoneById(follower.TimeZone)));
				}
				IRegionalSettingsProvider regSettings = regionalCache[follower.Culture + follower.TimeZone];

				String subject = this.textProvider.Get(regSettings, "NForum.Core.EventSubscribers.FollowerEventSubscriberForum", "Subject", new { User = follower, Topic = topic, Forum = forum });
				String body = this.textProvider.Get(regSettings, "NForum.Core.EventSubscribers.FollowerEventSubscriberForum", "Body", new { User = follower, Topic = topic, Forum = forum });

				MailType userMailType = type;
				if (follower.CustomPropertyExists("FollowerFrequency")) {
					Enum.TryParse<MailType>(follower.GetCustomPropertyString("FollowerFrequency"), out userMailType);
				}

				this.mailService.Send(subject, body, follower.EmailAddress, follower.Name, senderName, senderEmailAddress, userMailType);
			}
		}

		private void InformTopicFollowers(Topic topic, Post post) {
			IEnumerable<FollowTopic> followers = this.followerService.GetFollowers(topic);
			User user = this.userProvider.CurrentUser;

			String senderName = this.confService.Read().SenderName();
			String senderEmailAddress = this.confService.Read().SenderEmailAddress();
			MailType type = MailType.Weekly;
			if (this.confService.Read().CustomPropertyExists("DefaultFollowerFrequency")) {
				Enum.TryParse<MailType>(this.confService.Read().GetCustomPropertyString("DefaultFollowerFrequency"), out type);
			}

			Dictionary<String, IRegionalSettingsProvider> regionalCache = new Dictionary<String, IRegionalSettingsProvider>();
			foreach (FollowTopic ft in followers.Where(ft => ft.UserId != user.Id)) {
				User follower = ft.User;

				if (!regionalCache.ContainsKey(follower.Culture + follower.TimeZone)) {
					// We're going to need a "hard-coded" regional settings, using the regular provider will give us the settings of the current user!
					regionalCache.Add(follower.Culture + follower.TimeZone, new RegionalSettingsProvider(new CultureInfo(follower.Culture), TimeZoneInfo.FindSystemTimeZoneById(follower.TimeZone)));

				}
				IRegionalSettingsProvider regSettings = regionalCache[follower.Culture + follower.TimeZone];

				String subject = this.textProvider.Get(regSettings, "NForum.Core.EventSubscribers.FollowerEventSubscriberTopic", "Subject", new { User = follower, Post = post, Topic = topic });
				String body = this.textProvider.Get(regSettings, "NForum.Core.EventSubscribers.FollowerEventSubscriberTopic", "Body", new { User = follower, Post = post, Topic = topic });

				MailType userMailType = type;
				if (follower.CustomPropertyExists("FollowerFrequency")) {
					Enum.TryParse<MailType>(follower.GetCustomPropertyString("FollowerFrequency"), out userMailType);
				}

				this.mailService.Send(subject, body, follower.EmailAddress, follower.Name, senderName, senderEmailAddress, userMailType);
			}
		}

		public Byte Priority {
			get {
				return (Byte)EventPriority.Lowest;
			}
		}
	}
}