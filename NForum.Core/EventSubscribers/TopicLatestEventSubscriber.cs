using NForum.Core.Abstractions;
using NForum.Core.Abstractions.Data;
using NForum.Core.Abstractions.Events;
using NForum.Core.Events;
using NForum.Core.Events.Payloads;
using System;

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

		public void Handle(Object payload, IRequest request) {
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

		public void Handle(PostCreated payload, IRequest request) {
			Post post = this.postRepo.Read(payload.Post.Id);
			// Let's get the post in question, other subscribers might have updated it!
			if (post.State == PostState.None) {
				Topic topic = this.topicRepo.Read(payload.Post.TopicId);
				// Is the post newer than the latest on the parent topic?
				if (topic.LatestPost.Changed < payload.Post.Changed) {
					// Yes, well, then this is the latest!
					topic.LatestPost = payload.Post;
					this.topicRepo.Update(topic);
				}
			}
		}

		public void Handle(PostStateUpdated payload, IRequest request) {
			// TODO:
		}

		public void Handle(PostDeleted payload, IRequest request) {
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