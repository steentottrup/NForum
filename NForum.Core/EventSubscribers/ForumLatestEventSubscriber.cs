//using NForum.Core.Abstractions;
//using NForum.Core.Abstractions.Data;
//using NForum.Core.Abstractions.Events;
//using NForum.Core.Events;
//using NForum.Core.Events.Payloads;
//using System;
//using System.Linq;

//namespace NForum.Core.EventSubscribers {

//	public class ForumLatestEventSubscriber :
//												IEventSubscriber<TopicCreated>,
//												IEventSubscriber<TopicStateUpdated>,
//												IEventSubscriber<TopicDeleted>,
//												IEventSubscriber<PostCreated>,
//												IEventSubscriber<PostStateUpdated>,
//												IEventSubscriber<PostDeleted> {
//		protected readonly ILogger logger;
//		protected readonly ITopicRepository topicRepo;
//		protected readonly ITopicHistoryRepository topicHistRepo;
//		protected readonly IForumRepository forumRepo;

//		public ForumLatestEventSubscriber(IForumRepository forumRepo,
//											ITopicRepository topicRepo,
//											ITopicHistoryRepository topicHistRepo,
//											ILogger logger) {

//			this.forumRepo = forumRepo;
//			this.topicRepo = topicRepo;
//			this.topicHistRepo = topicHistRepo;
//			this.logger = logger;
//		}

//		public void Handle(Object payload, IState request) {
//			if (payload is TopicCreated) {
//				this.Handle((TopicCreated)payload, request);
//			}
//			else if (payload is TopicStateUpdated) {
//				this.Handle((TopicStateUpdated)payload, request);
//			}
//			else if (payload is TopicDeleted) {
//				this.Handle((TopicDeleted)payload, request);
//			}
//			else if (payload is PostCreated) {
//				this.Handle((PostCreated)payload, request);
//			}
//			else if (payload is PostStateUpdated) {
//				this.Handle((PostStateUpdated)payload, request);
//			}
//			else if (payload is PostDeleted) {
//				this.Handle((PostDeleted)payload, request);
//			}
//			else {
//				throw new ArgumentException("unknown payload");
//			}
//		}

//		public void Handle(TopicCreated payload, IState request) {
//			// Let's get the topic in question, other subscribers might have updated it!
//			Topic topic = this.topicRepo.Read(payload.Topic.Id);
//			// Is the topic still visible?
//			if (topic.IsVisible()) {
//				// Yes, let's get the parent forum.
//				Forum forum = this.forumRepo.Read(topic.ForumId);
//				// Is the topic the latest post/topic in the parent forum?
//				if (forum.Updated(topic, this.topicHistRepo)) {
//					// Yes, we changed something let's store the changes in the data-store.
//					this.forumRepo.Update(forum);
//				}
//			}
//		}

//		public void Handle(TopicStateUpdated payload, IState request) {
//			//			Topic topic = this.topicRepo.Read(payload.UpdatedTopic.Id);
//			//			// Did an actual change occure, or are we back to the original?
//			//			if ((payload.OriginalTopic.IsVisible() && !topic.IsVisible()) ||
//			//				(!payload.OriginalTopic.IsVisible() && topic.IsVisible())) {
//			//				// The topic was changed alright!
//			//				Forum forum = topic.Forum;
//			//				// Let's get the latest topic in the forum.
//			//				Topic latest = forum.Topics.Where(t => t.IsVisible())
//			//									.OrderByDescending(t => t.Created)
//			//									.FirstOrDefault();
//			//				// And the latest post.
//			//				Post latestPost = forum.Topics.Where(t => t.IsVisible() && t.LatestPost != null)
//			//									.OrderByDescending(t => t.LatestPost.Created)
//			//									.Select(t => t.LatestPost)
//			//									.FirstOrDefault();

//			//				//Boolean updated = false;
//			//				//// We got both, is the topic the latest?
//			//				//if (latest != null && latestPost != null && latestPost.Created <= latest.Created) {
//			//				//	updated = forum.UpdateLatest2(latest);
//			//				//	if (updated) {
//			//				//		this.UpdateParents(forum, latest);
//			//				//	}
//			//				//}
//			//				//else if (latest != null && latestPost != null && latest.Created <= latestPost.Created) {
//			//				//	updated = forum.UpdateLatest2(latestPost);
//			//				//	if (updated) {
//			//				//		this.UpdateParents(forum, latestPost);
//			//				//	}
//			//				//}
//			//				//else if (latestPost == null) {
//			//				//	updated = forum.UpdateLatest2(latest);
//			//				//	if (updated) {
//			//				//		this.UpdateParents(forum, latest);
//			//				//	}
//			//				//}
//			//				//else if (latest == null) {
//			//				//	updated = forum.UpdateLatest2(latestPost);
//			//				//	if (updated) {
//			//				//		this.UpdateParents(forum, latestPost);
//			//				//	}
//			//				//}

//			//				//if (updated) {
//			//				//	this.forumRepo.Update(forum);
//			//				//}
//			//			}
//		}

//		public void Handle(TopicDeleted payload, IState request) {
//			//			// Let's get the topic in question, other subscribers might have updated it!
//			//			Topic topic = this.topicRepo.Read(payload.Topic.Id);
//			//			Forum forum = topic.Forum;
//			//			if (forum.LatestTopic != null && forum.LatestTopic.Id == topic.Id) {
//			//				// This is the latest topic, so we need to update the forum!
//			//				Topic latest = forum.Topics.Where(t => t.IsVisible() && t.Id != topic.Id).OrderByDescending(t => t.Created).FirstOrDefault();
//			//				if (latest != null) {
//			//					forum.UpdateLatest(latest);
//			//				}
//			//				else {
//			//					forum.LatestTopic = null;
//			//					forum.LatestPost = null;
//			//				}
//			//				forumRepo.Update(forum);

//			//				this.UpdateParents(forum, latest);
//			//			}
//		}

//		public void Handle(PostCreated payload, IState request) {
//			//			// Let's get the post in question, other subscribers might have updated it!
//			//			Post post = this.postRepo.Read(payload.Post.Id);
//			//			if (post.State == PostState.None) {
//			//				Topic topic = this.topicRepo.Read(post.TopicId);
//			//				// Is the post newer than the latest in the topic?
//			//				if (topic.LatestPost == null || topic.LatestPost.Created <= post.Created) {
//			//					// Yes, well, then this is the latest!
//			//					topic.LatestPost = post;
//			//					this.topicRepo.Update(topic);

//			//					this.UpdateParents(topic.Forum, post);
//			//				}
//			//			}
//		}

//		public void Handle(PostStateUpdated payload, IState request) {
//			//			Post post = this.postRepo.Read(payload.UpdatedPost.Id);
//			//			// Did an actual change occure, or are we back to the original?
//			//			if ((payload.OriginalPost.IsVisible() && !post.IsVisible()) ||
//			//				(!payload.OriginalPost.IsVisible() && post.IsVisible())) {

//			//				Topic topic = post.Topic;
//			//				Post latest = topic.Posts.Where(p => p.IsVisible())
//			//									.OrderByDescending(p => p.Created)
//			//									.FirstOrDefault();

//			//				// Did we get a latest? Do we not have a latest on the topic?
//			//				// Or do we have one, but the latest is newer?
//			//				if (latest != null &&
//			//					(topic.LatestPost == null ||
//			//					(topic.LatestPost != null && topic.LatestPost.Id != latest.Id))) {

//			//					topic.LatestPost = latest;
//			//					this.topicRepo.Update(topic);

//			//					this.UpdateParents(topic.Forum, latest);
//			//					this.forumRepo.Update(topic.Forum);
//			//				}
//			//			}
//		}

//		public void Handle(PostDeleted payload, IState request) {
//			//			// Let's get the post in question, other subscribers might have updated it!
//			//			Post post = this.postRepo.Read(payload.Post.Id);
//			//			Topic topic = post.Topic;
//			//			if (topic.LatestPost != null && topic.LatestPost.Id == post.Id) {
//			//				// This is the latest post, so we need to update the topic!
//			//				Post latest = topic.Posts.Where(p => p.IsVisible() && p.Id != post.Id).OrderByDescending(p => p.Created).FirstOrDefault();
//			//				if (latest != null) {
//			//					topic.LatestPost = latest;
//			//				}
//			//				else {
//			//					topic.LatestPost = null;
//			//				}
//			//				topicRepo.Update(topic);

//			//				this.UpdateParents(topic.Forum, latest);
//			//			}
//		}

//		//		private void UpdateParents(Forum forum, Topic topic) {
//		//			while (forum.ParentForum != null) {
//		//				forum = forum.ParentForum;
//		//				if ((forum.LatestTopic == null && forum.LatestPost == null) ||
//		//					(forum.LatestTopic == null && forum.LatestPost != null && forum.LatestPost.Created <= topic.Created) ||
//		//					(forum.LatestPost == null && forum.LatestTopic != null && (forum.LatestTopic.Created <= topic.Created || !forum.LatestTopic.IsVisible()))) {

//		//					forum.UpdateLatest(topic);
//		//					this.forumRepo.Update(forum);
//		//				}
//		//				else {
//		//					break;
//		//				}
//		//			}
//		//		}

//		//		private void UpdateParents(Forum forum, Post post) {
//		//			// Empty forum (should never happen, this is a post, so we should have a topic) OR
//		//			// new post never than latest topic OR
//		//			// new post never than latest post
//		//			if ((forum.LatestPost == null && forum.LatestTopic == null) ||
//		//				(forum.LatestTopic != null && forum.LatestTopic.Created <= post.Created) ||
//		//				(forum.LatestPost != null && (!forum.LatestPost.IsVisible() || forum.LatestPost.Created <= post.Created))) {

//		//				forum.UpdateLatest(post);
//		//				this.forumRepo.Update(forum);

//		//				if (forum.ParentForum != null) {
//		//					this.UpdateParents(forum.ParentForum, post);
//		//				}
//		//			}
//		//		}

//		public Byte Priority {
//			get {
//				// We want to handle this at the end, when state etc. is sorted!
//				return (Byte)EventPriority.Lowest;
//			}
//		}
//	}
//}