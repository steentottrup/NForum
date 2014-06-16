using NForum.Core.Abstractions;
using NForum.Core.Abstractions.Events;
using NForum.Core.Events;
using NForum.Core.Events.Payloads;
using System;

namespace NForum.Core.EventSubscribers {

	public class ForumLatestEventSubscriber :
												IEventSubscriber<TopicCreated>,
												IEventSubscriber<TopicStateUpdated>,
												IEventSubscriber<TopicDeleted>,
												IEventSubscriber<PostCreated>,
												IEventSubscriber<PostStateUpdated>,
												IEventSubscriber<PostDeleted> {

		public void Handle(Object payload, IRequest request) {
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

		public void Handle(TopicCreated payload, IRequest request) {
			// TODO:
		}

		public void Handle(TopicStateUpdated payload, IRequest request) {
			// TODO:
		}

		public void Handle(TopicDeleted payload, IRequest request) {
			// TODO:
		}

		public void Handle(PostCreated payload, IRequest request) {
			// TODO:
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