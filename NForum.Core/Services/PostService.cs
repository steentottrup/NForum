using NForum.Core.Abstractions;
using NForum.Core.Abstractions.Data;
using NForum.Core.Abstractions.Events;
using NForum.Core.Abstractions.Providers;
using NForum.Core.Abstractions.Services;
using NForum.Core.Events.Payloads;
using System;
using System.Collections.Generic;

namespace NForum.Core.Services {

	public class PostService : IPostService {
		protected readonly IUserProvider userProvider;
		protected readonly IPostRepository postRepo;
		protected readonly ITopicRepository topicRepo;
		protected readonly IForumRepository forumRepo;
		protected readonly ILogger logger;
		protected readonly IEventPublisher eventPublisher;
		protected readonly IForumConfigurationService confService;
		protected readonly IPermissionService permService;

		public PostService(IUserProvider userProvider,
							IForumRepository forumRepo,
							ITopicRepository topicRepo,
							IPostRepository postRepo,
							IEventPublisher eventPublisher,
							ILogger logger,
							IPermissionService permService,
							IForumConfigurationService confService) {

			this.userProvider = userProvider;
			this.postRepo = postRepo;
			this.topicRepo = topicRepo;
			this.forumRepo = forumRepo;
			this.logger = logger;
			this.eventPublisher = eventPublisher;
			this.permService = permService;
			this.confService = confService;
		}

		/// <summary>
		/// Method for creating a new post as a reply to a topic.
		/// </summary>
		/// <param name="topic">The topic to create the new post in.</param>
		/// <param name="subject">The subject of the new post.</param>
		/// <param name="message">The message of the new post.</param>
		/// <param name="customProperties"></param>
		/// <returns>The new post.</returns>
		/// <exception cref="System.ArgumentNullException">Topic, subject and message are all required (not null) arguments.</exception>
		public Post Create(Topic topic, String subject, String message, IDictionary<String, Object> customProperties = null) {
			return this.Create(topic, null, subject, message, customProperties);
		}

		/// <summary>
		/// Method for creating a new post as a reply to an existing post.
		/// </summary>
		/// <param name="post">The post to create the new post in reply to.</param>
		/// <param name="subject">The subject of the new post.</param>
		/// <param name="message">The message of the new post.</param>
		/// <param name="customProperties"></param>
		/// <returns>The new post.</returns>
		/// <exception cref="System.ArgumentNullException">Post, subject and message are all required (not null) arguments.</exception>
		public Post Create(Post post, String subject, String message, IDictionary<String, Object> customProperties = null) {
			// Let's get the topic from the data-storage!
			Topic parentTopic = this.topicRepo.Read(post.TopicId);
			return this.Create(parentTopic, post, subject, message, customProperties);
		}

		protected Post Create(Topic topic, Post post, String subject, String message, IDictionary<String, Object> customProperties) {
			if (topic == null) {
				throw new ArgumentNullException("topic");
			}
			if (String.IsNullOrWhiteSpace(subject)) {
				throw new ArgumentNullException("subject");
			}
			if (String.IsNullOrWhiteSpace(message)) {
				throw new ArgumentNullException("message");
			}
			// Let's get the topic from the data-storage!
			topic = this.topicRepo.Read(topic.Id);
			if (topic == null) {
				throw new ArgumentException("topic does not exist");
			}
			if (post != null) {
				// Let's get the topic from the data-storage!
				post = this.postRepo.Read(post.Id);
				if (post == null) {
					throw new ArgumentException("post does not exist");
				}
			}

			this.logger.WriteFormat("Create called on PostService, subject: {0}, topic id: {1}", subject, topic.Id);
			AccessFlag flag = this.permService.GetAccessFlag(this.userProvider.CurrentUser, topic.Forum);
			if ((flag & AccessFlag.Reply) != AccessFlag.Reply) {
				this.logger.WriteFormat("User does not have permissions to create a new post in topic {1}, subject: {0}", subject, topic.Id);
				throw new PermissionException("post, create");
			}

			Post p = new Post {
				Author = this.userProvider.CurrentUser,
				AuthorId = this.userProvider.CurrentUser.Id,
				Changed = DateTime.UtcNow,
				Created = DateTime.UtcNow,
				Editor = this.userProvider.CurrentUser,
				EditorId = this.userProvider.CurrentUser.Id,
				Topic = topic,
				TopicId = topic.Id,
				Forum = topic.Forum,
				ForumId = topic.ForumId,
				Message = message,
				State = PostState.None,
				Subject = subject
			};
			p.SetCustomProperties(customProperties);

			// Was a parent post given?
			if (post != null) {
				// Let's store that then, for the "tree" view (and other features)
				p.ParentPost = post;
				p.ParentPostId = post.Id;
			}

			this.postRepo.Create(p);
			this.logger.WriteFormat("Post created in PostService, Id: {0}", p.Id);
			this.eventPublisher.Publish<PostCreated>(new PostCreated {
				Post = p
			});
			this.logger.WriteFormat("Create events in PostService fired, Id: {0}", p.Id);

			return p;
		}

		/// <summary>
		/// Method for reading a post.
		/// </summary>
		/// <param name="id">The id of the post needed.</param>
		/// <returns>The post with the given id, or null.</returns>
		public Post Read(Int32 id) {
			this.logger.WriteFormat("Read called on PostService, Id: {0}", id);
			Post post = this.postRepo.Read(id);
			if (!this.permService.HasAccess(this.userProvider.CurrentUser, post.Topic.Forum, (Int64)AccessFlag.Read)) {
				this.logger.WriteFormat("User does not have permissions to read the post, id: {0}", post.Id);
				throw new PermissionException("topic, read");
			}

			return post;
		}

		/// <summary>
		/// Method for reading a list of posts.
		/// </summary>
		/// <param name="topic">The topic the posts should be fetched from.</param>
		/// <param name="pageIndex">The page requested (0 indexed).</param>
		/// <returns>A list of posts from the given topic.</returns>
		public IEnumerable<Post> Read(Topic topic, Int32 pageIndex) {
			return this.Read(topic, pageIndex, false, false);
		}

		/// <summary>
		/// Method for reading a list of posts.
		/// </summary>
		/// <param name="topic">The topic the posts should be fetched from.</param>
		/// <param name="pageIndex">The page requested (0 indexed).</param>
		/// <param name="includeQuarantined">Include quarantined posts.</param>
		/// <param name="includeDeleted">Include deleted posts.</param>
		/// <returns>A list of posts from the given topic.</returns>
		public IEnumerable<Post> Read(Topic topic, Int32 pageIndex, Boolean includeQuarantined, Boolean includeDeleted) {
			if (topic == null) {
				throw new ArgumentNullException("topic");
			}
			Forum forum = this.forumRepo.Read(topic.ForumId);
			// We need to know how many posts to show per page, let's get the board!
			Int32 postsPerPage = this.confService.Read().PostsPerPage();
			// Let the repo get the posts, and return them!
			return this.postRepo.Read(topic, postsPerPage, pageIndex, includeQuarantined, includeDeleted);
		}

		/// <summary>
		/// Method for updating a post.
		/// </summary>
		/// <param name="post">The properties from this post will be update in the data storage.</param>
		/// <returns>The updated post.</returns>
		/// <remarks>Please note, only some properties are taken from the give post, others are automatically updated (changed, editor, etc.) and others can not be updated by this method (state, etc.).</remarks>
		public Post Update(Post post) {
			if (post == null) {
				throw new ArgumentNullException("post");
			}
			this.logger.WriteFormat("Update called on PostService, Id: {0}", post.Id);
			// Let's get the topic from the data-storage!
			Post oldPost = this.Read(post.Id);
			if (oldPost == null) {
				this.logger.WriteFormat("Update post failed, no post with the given id was found, Id: {0}", post.Id);
				throw new ArgumentException("psot does not exist");
			}
			Post originalPost = oldPost.Clone() as Post;

			// Author with "update" access or moderator?
			AccessFlag flag = this.permService.GetAccessFlag(this.userProvider.CurrentUser, oldPost.Topic.Forum);
			if ((flag & AccessFlag.Priority) != AccessFlag.Update) {
				this.logger.WriteFormat("User does not have permissions to update a post, id: {1}, subject: {0}", post.Subject, post.Id);
				throw new PermissionException("topic, update");
			}

			Boolean changed = false;
			if (oldPost.Subject != post.Subject) {
				oldPost.Subject = post.Subject;
				changed = true;
			}
			if (oldPost.Message != post.Message) {
				oldPost.Message = post.Message;
				changed = true;
			}
			if (oldPost.CustomProperties != post.CustomProperties) {
				oldPost.CustomProperties = post.CustomProperties;
				changed = true;
			}

			if (changed) {
				oldPost.Editor = this.userProvider.CurrentUser;
				oldPost.EditorId = this.userProvider.CurrentUser.Id;
				oldPost.Changed = DateTime.UtcNow;
				oldPost = this.postRepo.Update(oldPost);
				this.logger.WriteFormat("Post updated in postService, Id: {0}", oldPost.Id);
				this.eventPublisher.Publish<PostUpdated>(new PostUpdated(originalPost) {
					UpdatedPost = oldPost
				});
				this.logger.WriteFormat("Update events in PostService fired, Id: {0}", oldPost.Id);
			}
			return oldPost;
		}

		/// <summary>
		/// Method for updating state on a post.
		/// </summary>
		/// <param name="post">The post to update.</param>
		/// <param name="newState">The new state of the post.</param>
		/// <returns></returns>
		public Post Update(Post post, PostState newState) {
			if (post == null) {
				throw new ArgumentNullException("post");
			}
			this.logger.WriteFormat("Update called on PostService, Id: {0}", post.Id);
			// Let's get the topic from the data-storage!
			Post oldPost = this.Read(post.Id);
			if (oldPost == null) {
				this.logger.WriteFormat("Update post failed, no post with the given id was found, Id: {0}", post.Id);
				throw new ArgumentException("post does not exist");
			}

			Post originalPost = oldPost.Clone() as Post;
			// Has state changed?
			if (oldPost.State != newState) {
				oldPost.State = newState;

				oldPost.Editor = this.userProvider.CurrentUser;
				oldPost.EditorId = this.userProvider.CurrentUser.Id;
				oldPost.Changed = DateTime.UtcNow;
				oldPost = this.postRepo.Update(oldPost);
				this.logger.WriteFormat("Post updated in PostService, Id: {0}", oldPost.Id);
				this.eventPublisher.Publish<PostStateUpdated>(new PostStateUpdated(originalPost) {
					UpdatedPost = oldPost
				});
				this.logger.WriteFormat("Update events in PostService fired, Id: {0}", oldPost.Id);
			}
			return oldPost;
		}

		/// <summary>
		/// Method for deleting a post.
		/// </summary>
		/// <param name="post">The post to delete.</param>
		public void Delete(Post post) {
			if (post == null) {
				throw new ArgumentNullException("post");
			}
			this.logger.WriteFormat("Delete called on PostService, Id: {0}", post.Id);
			if (!this.permService.HasAccess(this.userProvider.CurrentUser, post.Topic.Forum, (Int64)AccessFlag.Delete)) {
				this.logger.WriteFormat("User does not have permissions to delete a post, id: {1}, subject: {0}", post.Subject, post.Id);
				throw new PermissionException("post, delete");
			}
			// TODO: posts, attachments, etc
			this.postRepo.Delete(post);
			this.eventPublisher.Publish<PostDeleted>(new PostDeleted {
				Post = post
			});
			this.logger.WriteFormat("Delete events in PostService fired, Id: {0}", post.Id);
		}
	}
}