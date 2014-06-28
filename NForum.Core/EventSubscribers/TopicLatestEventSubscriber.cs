using NForum.Core.Abstractions;
using NForum.Core.Abstractions.Data;
using NForum.Core.Abstractions.Events;
using NForum.Core.Events;
using NForum.Core.Events.Payloads;
using System;
using System.Linq;

namespace NForum.Core.EventSubscribers {

	public class TopicLatestEventSubscriber :
												IEventSubscriber<PostCreated>,
												IEventSubscriber<PostStateUpdated>,
												IEventSubscriber<PostDeleted> {
		protected readonly ITopicRepository topicRepo;
		protected readonly IPostRepository postRepo;
		protected readonly ILogger logger;

		public TopicLatestEventSubscriber(ITopicRepository topicRepo,
											IPostRepository postRepo,
											ILogger logger) {
			this.topicRepo = topicRepo;
			this.postRepo = postRepo;
			this.logger = logger;
		}

		public void Handle(Object payload, IState request) {
			if (payload is PostCreated) {
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

		public void Handle(PostCreated payload, IState request) {
			// Let's get the post in question, other subscribers might have updated it!
			Post post = this.postRepo.Read(payload.Post.Id);
			// Is the post visible?
			if (post.IsVisible()) {
				// Yes, okay, let's get the topic!
				Topic topic = this.topicRepo.Read(payload.Post.TopicId);
				// Is the post newer than the latest on the parent topic?
				if (topic.LatestPost == null || topic.LatestPost.Created < payload.Post.Created) {
					// Yes, well, then this is the latest!
					topic.LatestPost = payload.Post;
					// Update the topic!
					this.topicRepo.Update(topic);
				}
			}
		}

		public void Handle(PostStateUpdated payload, IState request) {
			// Let's get the post in question, other subscribers might have updated it!
			Post post = this.postRepo.Read(payload.UpdatedPost.Id);
			// Did an actual change occure, or are we back to the original?
			if ((post.IsVisible() && !payload.OriginalPost.IsVisible()) ||
				(!post.IsVisible() && payload.OriginalPost.IsVisible())) {

				// Yes, a state change has happened, let's get the topic
				Topic topic = post.Topic;
				// And the latest, visible post on the topic.
				Post latest = topic.Posts.Where(p => p.IsVisible()).OrderByDescending(p => p.Created).FirstOrDefault();
				// Should we change the latest post on the topic?
				if (topic.LatestPost != latest) {
					// Yes!
					topic.LatestPost = latest;
					// Update it!
					this.topicRepo.Update(topic);
				}
			}
		}

		public void Handle(PostDeleted payload, IState request) {
			// Let's get the post in question, other subscribers might have updated it!
			Post post = this.postRepo.Read(payload.Post.Id);
			Topic topic = post.Topic;
			if (topic.LatestPost.Id == post.Id) {
				// This is the latest post, so we need to update the topic!
				Post latest = topic.Posts.Where(p => p.IsVisible() && p.Id != post.Id).OrderByDescending(p => p.Created).FirstOrDefault();
				if (latest != null) {
					topic.LatestPost = latest;
				}
				else {
					topic.LatestPost = null;
				}
				topicRepo.Update(topic);
			}
		}

		public Byte Priority {
			get {
				// We want to handle this at the end, when state etc. is sorted!
				return (Byte)EventPriority.Lowest;
			}
		}
	}
}