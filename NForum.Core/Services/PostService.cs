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
		protected readonly IBoardRepository boardRepo;
		protected readonly ILogger logger;
		protected readonly IEventPublisher eventPublisher;

		public PostService(IUserProvider userProvider,
							IPostRepository postRepo,
							ITopicRepository topicRepo,
							IForumRepository forumRepo,
							IBoardRepository boardRepo,
							ILogger logger,
							IEventPublisher eventPublisher) {

			this.userProvider = userProvider;
			this.postRepo = postRepo;
			this.topicRepo = topicRepo;
			this.forumRepo = forumRepo;
			this.boardRepo = boardRepo;
			this.logger = logger;
			this.eventPublisher = eventPublisher;
		}

		/// <summary>
		/// Method for creating a new post as a reply to a topic.
		/// </summary>
		/// <param name="topic">The topic to create the new post in.</param>
		/// <param name="subject">The subject of the new post.</param>
		/// <param name="message">The message of the new post.</param>
		/// <returns>The new post.</returns>
		/// <exception cref="System.ArgumentNullException">Topic, subject and message are all required (not null) arguments.</exception>
		public Post Create(Topic topic, String subject, String message) {
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
			Topic oldTopic = this.topicRepo.Read(topic.Id);
			return this.Create(oldTopic, null, subject, message);
		}

		/// <summary>
		/// Method for creating a new post as a reply to an existing post.
		/// </summary>
		/// <param name="post">The post to create the new post in reply to.</param>
		/// <param name="subject">The subject of the new post.</param>
		/// <param name="message">The message of the new post.</param>
		/// <returns>The new post.</returns>
		/// <exception cref="System.ArgumentNullException">Post, subject and message are all required (not null) arguments.</exception>
		public Post Create(Post post, String subject, String message) {
			if (post == null) {
				throw new ArgumentNullException("post");
			}
			if (String.IsNullOrWhiteSpace(subject)) {
				throw new ArgumentNullException("subject");
			}
			if (String.IsNullOrWhiteSpace(message)) {
				throw new ArgumentNullException("message");
			}
			// Let's get the topic from the data-storage!
			Topic parentTopic = this.topicRepo.Read(post.TopicId);
			// Let's get the post from the data-storage!
			Post oldPost = this.postRepo.Read(post.Id);
			return this.Create(parentTopic, oldPost, subject, message);
		}

		private Post Create(Topic topic, Post post, String subject, String message) {
			if (!this.HasAccess(topic.ForumId, AccessFlag.Reply)) {
				this.logger.WriteFormat("User does not have permissions to create a new post, subject: {0}", subject);
				throw new PermissionException("reply access");
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
				Message = message,
				State = PostState.None,
				Subject = subject
			};

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
			return this.postRepo.Read(id);
		}

		/// <summary>
		/// Method for reading a list of posts.
		/// </summary>
		/// <param name="topic">The topic the posts should be fetched from.</param>
		/// <param name="pageIndex">The page requested (0 indexed).</param>
		/// <returns>A list of posts from the given topic.</returns>
		public IEnumerable<Post> Read(Topic topic, Int32 pageIndex) {
			if (topic == null) {
				throw new ArgumentNullException("topic");
			}
			Forum forum = this.forumRepo.Read(topic.ForumId);
			// We need to know how many posts to show per page, let's get the board!
			Board board = this.boardRepo.ByForum(forum);
			// Let the repo get the posts, and return them!
			return this.postRepo.Read(topic, board.PostsPerPage, pageIndex);
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
			Board board = this.boardRepo.ByForum(forum);
			// Let the repo get the posts, and return them!
			return this.postRepo.Read(topic, board.PostsPerPage, pageIndex, includeQuarantined, includeDeleted);
		}

		/// <summary>
		/// Method for updating a post.
		/// </summary>
		/// <param name="post">The properties from this post will be update in the data storage.</param>
		/// <returns>The updated post.</returns>
		/// <remarks>Please note, only some properties are taken from the give post, others are automatically updated (changed, editor, etc.) and others can not be updated by this method (state, etc.).</remarks>
		public Post Update(Post post) {
			return this.Update(post, false);
		}

		/// <summary>
		/// Method for deleting a post.
		/// </summary>
		/// <param name="post">The post to delete.</param>
		public void Delete(Post post) {
			if (post == null) {
				throw new ArgumentNullException("post");
			}
			// TODO: Permissions!?!?!?
			this.postRepo.Delete(post);
		}

		private Post Update(Post post, Boolean updateState) {
			if (post == null) {
				throw new ArgumentNullException("post");
			}

			Post oldPost = this.postRepo.Read(post.Id);
			Post originalPost = oldPost.Clone() as Post;
			if (oldPost != null) {
				Forum forum = this.forumRepo.ByPost(post);
				// Author with "update" access or moderator?
				if (!(this.HasAccess(forum.Id, AccessFlag.Update) && post.AuthorId == this.userProvider.CurrentUser.Id) &&
					!this.IsModerator(forum.Id)) {

					throw new PermissionException("Not author and not moderator");
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
				if (post.CustomProperties != oldPost.CustomProperties) {
					oldPost.CustomProperties = post.CustomProperties;
					changed = true;
				}
				if (updateState && oldPost.State != post.State) {
					// TODO: Permissions?!??!
					oldPost.State = post.State;
					changed = true;
				}

				if (changed) {
					oldPost.Editor = this.userProvider.CurrentUser;
					oldPost.EditorId = this.userProvider.CurrentUser.Id;
					oldPost.Changed = DateTime.UtcNow;
					oldPost = this.postRepo.Update(oldPost);
					this.logger.WriteFormat("Post updated in PostService, Id: {0}", post.Id);
					this.eventPublisher.Publish<PostUpdated>(new PostUpdated {
						Post = originalPost,
						UpdatedPost = oldPost
					});
					this.logger.WriteFormat("Update events in PostService fired, Id: {0}", post.Id);
				}

				return oldPost;
			}
			this.logger.WriteFormat("Update post failed, no post with the given id was found, Id: {0}", post.Id);
			// TODO:
			throw new ApplicationException();
		}

		private Boolean HasAccess(Int32 forumId, AccessFlag flag) {
			Forum forum = this.forumRepo.Read(forumId);
			// TODO:
			return true;
		}

		private Boolean IsModerator(Int32 forumId) {
			return this.HasAccess(forumId, AccessFlag.Moderator);
		}
	}
}