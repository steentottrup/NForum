using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NForum.Core;
using NForum.Core.Abstractions;
using NForum.Core.Abstractions.Data;
using NForum.Core.Abstractions.Events;
using NForum.Core.Abstractions.Providers;
using NForum.Core.Abstractions.Services;
using NForum.Core.Events;
using NForum.Core.Services;
using NForum.Persistence.EntityFramework;
using NForum.Persistence.EntityFramework.Repositories;
using NForum.Tests.CommonMocks;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading;

namespace NForum.Tests.BasicStructure {

	[TestClass]
	public class LatestPostTopicTests {
		private static UnitOfWork uow;

		[ClassInitialize]
		public static void SetUp(TestContext context) {
			DbConnection connection = Effort.DbConnectionFactory.CreateTransient();
			uow = new UnitOfWork(connection);
		}

		[ClassCleanup]
		public static void TearDown() {
			uow.Dispose();
			uow = null;
		}

		private void GetTopicService(UnitOfWork uow, out ICategoryService categoryService, out IForumService forumService, out ITopicService topicService, out IPostService postService) {
			ICategoryRepository cateRepo = new CategoryRepository(uow);
			IForumRepository forumRepo = new ForumRepository(uow);
			ITopicRepository topicRepo = new TopicRepository(uow);
			IPostRepository postRepo = new PostRepository(uow);
			IForumConfigurationRepository confRepo = new ForumConfigurationRepository(uow);

			IState request = new DummyRequest();

			ILogger logger = new ConsoleLogger();

			IUserRepository userRepo = new UserRepository(uow);
			User user = userRepo.Create(new User {
				Name = "D. Ummy",
				ProviderId = "12345678",
				FullName = "Mr. Doh Ummy",
				EmailAddress = "d@umm.y",
				Culture = "th-TH",
				TimeZone = "GMT Standard Time"
			});

			List<IEventSubscriber> subscribers = new List<IEventSubscriber>();

			IEventPublisher eventPublisher = new EventPublisher(subscribers, logger, request);
			IUserProvider userProvider = new DummyUserProvider(user);
			IPermissionService permService = new PermissionService();

			IForumConfigurationService confService = new ForumConfigurationService(confRepo);

			categoryService = new CategoryService(userProvider, cateRepo, eventPublisher, logger, permService);
			forumService = new ForumService(userProvider, cateRepo, forumRepo, topicRepo, postRepo, eventPublisher, logger, permService);
			topicService = new TopicService(userProvider, forumRepo, topicRepo, postRepo, eventPublisher, logger, permService, confService);
			postService = new PostService(userProvider, forumRepo, topicRepo, postRepo, eventPublisher, logger, permService, confService);
		}

		/// <summary>
		/// Create a topic in a forum, check it's returned as the latest topic on the forum!
		/// </summary>
		[TestMethod]
		public void LatestTopic1() {
			ICategoryService categoryService;
			IForumService forumService;
			ITopicService topicService;
			IPostService postService;
			this.GetTopicService(uow, out categoryService, out forumService, out topicService, out postService);

			Category category = categoryService.Create("Latest test category", "meh", 100);
			Forum forum = forumService.Create(category, "Latest test forum", "meh", 101);

			Topic first = topicService.Create(forum, "The first one", "bla bla bla");
			Topic latest = forumService.GetLatestTopic(forum, true);

			latest.Should().NotBeNull("a topic has been created");
			latest.Id.Should().Be(first.Id, "is the first topic");
		}

		/// <summary>
		/// Create 2 topics and make sure the last created, will be returned as the latest
		/// </summary>
		[TestMethod]
		public void LatestTopic2() {
			ICategoryService categoryService;
			IForumService forumService;
			ITopicService topicService;
			IPostService postService;
			this.GetTopicService(uow, out categoryService, out forumService, out topicService, out postService);

			Category category = categoryService.Create("Latest test category", "meh", 100);
			Forum forum = forumService.Create(category, "Latest test forum", "meh", 101);

			Topic first = topicService.Create(forum, "The first one", "bla bla bla");
			// TODO: Hm...
			Thread.Sleep(50);
			Topic second = topicService.Create(forum, "The second one", "bla bla bla");
			Topic latest = forumService.GetLatestTopic(forum, true);

			latest.Should().NotBeNull("2 topics has been created");
			latest.Id.Should().Be(second.Id, "the second topic should be the latest");
		}

		/// <summary>
		/// Create 2 topics and make sure the first one is the latest if the second is marked as not visible (quarantined).
		/// </summary>
		[TestMethod]
		public void LatestTopic3() {
			ICategoryService categoryService;
			IForumService forumService;
			ITopicService topicService;
			IPostService postService;
			this.GetTopicService(uow, out categoryService, out forumService, out topicService, out postService);

			Category category = categoryService.Create("Latest test category", "meh", 100);
			Forum forum = forumService.Create(category, "Latest test forum", "meh", 101);

			Topic first = topicService.Create(forum, "The first one", "bla bla bla");
			// TODO: Hm...
			Thread.Sleep(50);
			Topic second = topicService.Create(forum, "The second one", "bla bla bla");
			Topic latest = forumService.GetLatestTopic(forum, true);

			latest.Should().NotBeNull("2 topics are visible");
			latest.Id.Should().Be(second.Id, "the second topic is visible");

			topicService.Update(second, TopicState.Quarantined);
			latest = forumService.GetLatestTopic(forum, true);

			latest.Should().NotBeNull("1 topic is still visible");
			latest.Id.Should().Be(first.Id, "the second topic is no longer visible");
		}

		/// <summary>
		/// Create 2 topics and make sure the second one is the latest after making it invisible and visible again
		/// </summary>
		[TestMethod]
		public void LatestTopic4() {
			ICategoryService categoryService;
			IForumService forumService;
			ITopicService topicService;
			IPostService postService;
			this.GetTopicService(uow, out categoryService, out forumService, out topicService, out postService);

			Category category = categoryService.Create("Latest test category", "meh", 100);
			Forum forum = forumService.Create(category, "Latest test forum", "meh", 101);

			Topic first = topicService.Create(forum, "The first one", "bla bla bla");
			// TODO: Hm...
			Thread.Sleep(50);
			Topic second = topicService.Create(forum, "The second one", "bla bla bla");
			Topic latest = forumService.GetLatestTopic(forum, true);

			topicService.Update(second, TopicState.Quarantined);
			latest = forumService.GetLatestTopic(forum, true);

			latest.Should().NotBeNull("1 topic is still visible");
			latest.Id.Should().Be(first.Id, "the second topic is no longer visible");

			topicService.Update(second, TopicState.None);
			latest = forumService.GetLatestTopic(forum, true);

			latest.Should().NotBeNull("both topics are visible");
			latest.Id.Should().Be(second.Id, "the second topic is visible");
		}

		[TestMethod]
		public void LatestPost1() {
			ICategoryService categoryService;
			IForumService forumService;
			ITopicService topicService;
			IPostService postService;
			this.GetTopicService(uow, out categoryService, out forumService, out topicService, out postService);

			Category category = categoryService.Create("Latest test category part 2", "meh", 100);
			Forum forum = forumService.Create(category, "Latest test forum part 2", "meh", 101);

			Topic first = topicService.Create(forum, "The first one part 2", "bla bla bla");
			Post latestPost = topicService.GetLatestPost(first);

			latestPost.Should().BeNull("becase no post was created yet");

			Post firstPost = postService.Create(first, "My first post", "The body!");

			latestPost = forumService.GetLatestPost(forum, true);
			latestPost.Should().NotBeNull("becase a post was created in the forum");
			latestPost.Id.Should().Be(firstPost.Id, "only one post exists on the topic");
		}

		[TestMethod]
		public void LatestPost2() {
			ICategoryService categoryService;
			IForumService forumService;
			ITopicService topicService;
			IPostService postService;
			this.GetTopicService(uow, out categoryService, out forumService, out topicService, out postService);

			Category category = categoryService.Create("Latest test category part 2", "meh", 100);
			Forum forum = forumService.Create(category, "Latest test forum part 2", "meh", 101);

			Topic first = topicService.Create(forum, "The first one part 2", "bla bla bla");

			Post firstPost = postService.Create(first, "My first post", "The body!");
			// TODO: Hm...
			Thread.Sleep(50);
			Post secondPost = postService.Create(first, "My second post", "The body!");
			Post latestPost = topicService.GetLatestPost(first);

			latestPost.Should().NotBeNull("becase 2 posts was created on the topic");
			latestPost.Id.Should().Be(secondPost.Id, "the last post created");
		}

		[TestMethod]
		public void LatestPost3() {
			ICategoryService categoryService;
			IForumService forumService;
			ITopicService topicService;
			IPostService postService;
			this.GetTopicService(uow, out categoryService, out forumService, out topicService, out postService);

			Category category = categoryService.Create("Latest test category part 2", "meh", 100);
			Forum forum = forumService.Create(category, "Latest test forum part 2", "meh", 101);

			Topic first = topicService.Create(forum, "The first one part 2", "bla bla bla");

			Post firstPost = postService.Create(first, "My first post", "The body!");
			// TODO: Hm...
			Thread.Sleep(50);
			Post secondPost = postService.Create(first, "My second post", "The body!");

			secondPost = postService.Update(secondPost, PostState.Quarantined);
			
			Post latestPost = topicService.GetLatestPost(first);

			latestPost.Should().NotBeNull("becase 2 posts was created on the topic, one visible");
			latestPost.Id.Should().Be(firstPost.Id, "the last post created is no longer visible");
		}

		[TestMethod]
		public void LatestPost4() {
			ICategoryService categoryService;
			IForumService forumService;
			ITopicService topicService;
			IPostService postService;
			this.GetTopicService(uow, out categoryService, out forumService, out topicService, out postService);

			Category category = categoryService.Create("Latest test category part 2", "meh", 100);
			Forum forum = forumService.Create(category, "Latest test forum part 2", "meh", 101);

			Topic first = topicService.Create(forum, "The first one part 2", "bla bla bla");

			Post firstPost = postService.Create(first, "My first post", "The body!");
			// TODO: Hm...
			Thread.Sleep(50);
			Post secondPost = postService.Create(first, "My second post", "The body!");

			secondPost = postService.Update(secondPost, PostState.Quarantined);

			secondPost = postService.Update(secondPost, PostState.None);

			Post latestPost = topicService.GetLatestPost(first);

			latestPost.Should().NotBeNull("because 2 posts was created on the topic, both visible");
			latestPost.Id.Should().Be(secondPost.Id, "the last post created is again visible");
		}

		//		[TestMethod]
		//		public void ParentForumLatestTopic() {
		//			using (UnitOfWork uow = new UnitOfWork()) {
		//				ICategoryService categoryService;
		//				IForumService forumService;
		//				ITopicService topicService;
		//				IPostService postService;
		//				this.GetTopicService(uow, out categoryService, out forumService, out topicService, out postService);

		//				Category category = categoryService.Create("Latest test category child forum version", "meh", 100);
		//				Forum forum1 = forumService.Create(category, "Latest test forum child forum version", "meh", 101);
		//				Forum forumChild = forumService.Create(forum1, "Latest test forum child forum version - child", "meh", 101);

		//				// latest on both
		//				Topic first = topicService.Create(forumChild, "The first one child forum version", "bla bla bla");
		//				Thread.Sleep(10);
		//				Assert.AreEqual(first.Id, forumChild.LatestTopicId, "first topic is latest on forum");
		//				Assert.AreEqual(first.Id, forum1.LatestTopicId, "first topic is latest on forum");

		//				// only latest on forum1
		//				Topic second = topicService.Create(forum1, "The second one child forum version", "bla bla bla");
		//				Thread.Sleep(10);
		//				Assert.AreEqual(second.Id, forum1.LatestTopicId, "second topic is latest on forum");
		//				Assert.AreEqual(first.Id, forumChild.LatestTopicId, "first topic is latest on forum");

		//				// latest on both
		//				Topic third = topicService.Create(forumChild, "The third one child forum version", "bla bla bla");
		//				Assert.AreEqual(third.Id, forumChild.LatestTopicId, "thirdtopic is latest on forum");
		//				Assert.AreEqual(third.Id, forum1.LatestTopicId, "third topic is latest on forum");

		//				topicService.Update(third, TopicState.Quarantined);
		//				Assert.AreEqual(second.Id, forum1.LatestTopicId, "second topic is latest on forum");
		//				Assert.AreEqual(first.Id, forumChild.LatestTopicId, "first topic is latest on forum");
		//			}
		//		}
	}
}
