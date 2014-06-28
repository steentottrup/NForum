using NForum.Core.Abstractions;
using NForum.Core.Abstractions.Data;
using NForum.Core.Abstractions.Events;
using NForum.Core.Events;
using NForum.Core.Events.Payloads;
using System;
using System.Linq;

namespace NForum.Core.EventSubscribers {

	public class ForumLatestEventSubscriber :
												IEventSubscriber<TopicCreated>,
												IEventSubscriber<TopicStateUpdated>,
												IEventSubscriber<TopicDeleted>,
												IEventSubscriber<PostCreated>,
												IEventSubscriber<PostStateUpdated>,
												IEventSubscriber<PostDeleted> {
		protected readonly IForumRepository forumRepo;
		protected readonly ITopicRepository topicRepo;
		protected readonly IPostRepository postRepo;
		protected readonly ILogger logger;

		public ForumLatestEventSubscriber(IForumRepository forumRepo,
											ITopicRepository topicRepo,
											IPostRepository postRepo,
											ILogger logger) {

			this.forumRepo = forumRepo;
			this.topicRepo = topicRepo;
			this.postRepo = postRepo;
			this.logger = logger;
		}

		public void Handle(Object payload, IState request) {
			if (payload is TopicCreated) {
				this.Handle((TopicCreated)payload, request);
			}
			else if (payload is TopicStateUpdated) {
				this.Handle((TopicStateUpdated)payload, request);
			}
			else if (payload is TopicDeleted) {
				this.Handle((TopicDeleted)payload, request);
			}
			else if (payload is PostCreated) {
				this.Handle((PostCreated)payload, request);
			}
			else if (payload is PostStateUpdated) {
				this.Handle((PostStateUpdated)payload, request);
			}
			else if (payload is PostDeleted) {
				this.Handle((PostDeleted)payload, request);
			}
			else {
				throw new ArgumentException("unknown payload");
			}
		}

		public void Handle(TopicCreated payload, IState request) {
			// Let's get the topic in question, other subscribers might have updated it!
			Topic topic = this.topicRepo.Read(payload.Topic.Id);
			if (topic.State == TopicState.None) {
				Forum forum = this.forumRepo.Read(topic.ForumId);
				// Is the topic newer than the latest in the parent forum?
				if (forum.LatestTopic == null || forum.LatestTopic.Created < topic.Created) {
					// Yes, well, then this is the latest!
					forum.LatestTopic = topic;
					forum.LatestPost = null;
					this.forumRepo.Update(forum);

					this.UpdateParents(forum, topic);
				}
			}
		}

		public void Handle(TopicStateUpdated payload, IState request) {
			Topic topic = this.topicRepo.Read(payload.UpdatedTopic.Id);
			// Did an actual change occure, or are we back to the original?
			if ((payload.OriginalTopic.IsVisible() && !topic.IsVisible()) ||
				(!payload.OriginalTopic.IsVisible() && topic.IsVisible())) {

				Forum forum = topic.Forum;
				Topic latest = forum.Topics.Where(t => t.IsVisible())
									.OrderByDescending(t => t.Created)
									.FirstOrDefault();
				if (forum.LatestTopic != latest) {
					forum.LatestTopic = latest;
					forum.LatestPost = null;
					this.forumRepo.Update(forum);

					this.UpdateParents(forum, latest);
				}
			}
		}

		private void UpdateParents(Forum forum, Topic topic) {
			while (forum.ParentForum != null) {
				forum = forum.ParentForum;
				if (forum.LatestTopic == null || forum.LatestTopic.Created < topic.Created) {
					forum.LatestTopic = topic;
					forum.LatestPost = null;
					this.forumRepo.Update(forum);
				}
				else {
					break;
				}
			}
		}

		public void Handle(TopicDeleted payload, IState request) {
			// Let's get the forum in question, other subscribers might have updated it!
			Topic topic = this.topicRepo.Read(payload.Topic.Id);
			Forum forum = topic.Forum;
			if (forum.LatestTopic.Id == topic.Id) {
				// This is the latest topic, so we need to update the forum!
				Topic latest = forum.Topics.Where(t => t.IsVisible() && t.Id != topic.Id).OrderByDescending(t => t.Created).FirstOrDefault();
				if (latest != null) {
					forum.LatestTopic = latest;
					// TODO: Latest post!
				}
				else {
					forum.LatestTopic = null;
					// TODO: Latest post!
				}
				forumRepo.Update(forum);

				this.UpdateParents(forum, latest);
			}
			// TODO:
		}

		public void Handle(PostCreated payload, IState request) {
			// TODO:
		}

		public void Handle(PostStateUpdated payload, IState request) {
			// TODO:
		}

		public void Handle(PostDeleted payload, IState request) {
			// TODO:
		}

		public Byte Priority {
			get {
				// We want to handle this at the end, when state etc. is sorted!
				return (Byte)EventPriority.Lowest;
			}
		}
	}
}