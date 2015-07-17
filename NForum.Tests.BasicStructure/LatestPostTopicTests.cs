//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using NForum.Core;
//using NForum.Core.Abstractions;
//using NForum.Core.Abstractions.Data;
//using NForum.Core.Abstractions.Events;
//using NForum.Core.Abstractions.Providers;
//using NForum.Core.Abstractions.Services;
//using NForum.Core.Events;
//using NForum.Core.Services;
//using NForum.Persistence.EntityFramework;
//using NForum.Persistence.EntityFramework.Repositories;
//using NForum.Tests.CommonMocks;
//using System.Collections.Generic;
//using System.Data.Common;

//namespace NForum.Tests.BasicStructure {

//	[TestClass]
//	public class LatestPostTopicTests {
//		private static UnitOfWork uow;

//		[ClassInitialize]
//		public static void SetUp(TestContext context) {
//			DbConnection connection = Effort.DbConnectionFactory.CreateTransient();
//			uow = new UnitOfWork(connection);
//		}

//		[ClassCleanup]
//		public static void TearDown() {
//			uow.Dispose();
//			uow = null;
//		}

//		private void GetTopicService(UnitOfWork uow, out ICategoryService categoryService, out IForumService forumService, out ITopicService topicService, out IPostService postService) {
//			ICategoryRepository cateRepo = new CategoryRepository(uow);
//			IForumRepository forumRepo = new ForumRepository(uow);
//			ITopicRepository topicRepo = new TopicRepository(uow);
//			IPostRepository postRepo = new PostRepository(uow);
//			IForumConfigurationRepository confRepo = new ForumConfigurationRepository(uow);

//			IState request = new DummyRequest();

//			ILogger logger = new ConsoleLogger();

//			IUserRepository userRepo = new UserRepository(uow);
//			User user = userRepo.Create(new User {
//				Name = "D. Ummy",
//				ProviderId = "12345678",
//				FullName = "Mr. Doh Ummy",
//				EmailAddress = "d@umm.y",
//				Culture = "th-TH",
//				TimeZone = "GMT Standard Time"
//			});

//			List<IEventSubscriber> subscribers = new List<IEventSubscriber>();
//			// TODO:
//			//subscribers.Add(new ForumLatestEventSubscriber(forumRepo, topicRepo, postRepo, logger));

//			IEventPublisher eventPublisher = new EventPublisher(subscribers, logger, request);
//			IUserProvider userProvider = new DummyUserProvider(user);
//			IPermissionService permService = new PermissionService();

//			IForumConfigurationService confService = new ForumConfigurationService(confRepo);

//			categoryService = new CategoryService(userProvider, cateRepo, eventPublisher, logger, permService);
//			forumService = new ForumService(userProvider, cateRepo, forumRepo, eventPublisher, logger, permService);
//			topicService = new TopicService(userProvider, forumRepo, topicRepo, eventPublisher, logger, permService, confService);
//			postService = new PostService(userProvider, forumRepo, topicRepo, postRepo, eventPublisher, logger, permService, confService);
//		}

//		[TestMethod]
//		public void LatestTopics() {
//			ICategoryService categoryService;
//			IForumService forumService;
//			ITopicService topicService;
//			IPostService postService;
//			this.GetTopicService(uow, out categoryService, out forumService, out topicService, out postService);

//			Category category = categoryService.Create("Latest test category", "meh", 100);
//			Forum forum = forumService.Create(category, "Latest test forum", "meh", 101);

//			Topic first = topicService.Create(forum, "The first one", "bla bla bla");
//			Assert.AreEqual(first.Id, forum.LatestTopicId, "first post latest");

//			Topic second = topicService.Create(forum, "The second one", "bla bla bla");
//			Assert.AreEqual(second.Id, forum.LatestTopicId, "second post latest");

//			topicService.Update(second, TopicState.Quarantined);
//			Assert.AreEqual(first.Id, forum.LatestTopicId, "first post latest again");

//			topicService.Update(second, TopicState.None);
//			Assert.AreEqual(second.Id, forum.LatestTopicId, "second post latest again");
//		}

//		[TestMethod]
//		public void LatestPosts() {
//			using (UnitOfWork uow = new UnitOfWork()) {
//				ICategoryService categoryService;
//				IForumService forumService;
//				ITopicService topicService;
//				IPostService postService;
//				this.GetTopicService(uow, out categoryService, out forumService, out topicService, out postService);

//				Category category = categoryService.Create("Latest test category part 2", "meh", 100);
//				Forum forum = forumService.Create(category, "Latest test forum part 2", "meh", 101);

//				Topic first = topicService.Create(forum, "The first one part 2", "bla bla bla");
//				Assert.AreEqual(first.Id, forum.LatestTopicId, "first topic is latest on forum");
//				Assert.AreEqual(null, first.LatestPost, "no latest post on topic");
//				Assert.AreEqual(null, forum.LatestPost, "no latest post on forum");

//				Topic second = topicService.Create(forum, "The second one part 2", "bla bla bla");
//				Assert.AreEqual(second.Id, forum.LatestTopicId);
//				Assert.AreEqual(null, second.LatestPost);
//				Assert.AreEqual(null, forum.LatestPost);

//				Post newPost1 = postService.Create(first, "no 1", "bla bla");
//				Assert.AreEqual(null, forum.LatestTopic);
//				Assert.AreEqual(newPost1.Id, first.LatestPostId);
//				Assert.AreEqual(newPost1.Id, forum.LatestPostId);

//				Post newPost2 = postService.Create(second, "no 2", "bla bla");
//				Assert.AreEqual(null, forum.LatestTopic);
//				Assert.AreEqual(newPost2.Id, second.LatestPostId);
//				Assert.AreEqual(newPost2.Id, forum.LatestPostId);

//				Post newPost3 = postService.Create(second, "no 3", "bla bla");
//				Assert.AreEqual(null, forum.LatestTopic);
//				Assert.AreEqual(newPost3.Id, second.LatestPostId);
//				Assert.AreEqual(newPost3.Id, forum.LatestPostId);

//				postService.Update(newPost1, PostState.Quarantined);
//				Assert.AreEqual(null, forum.LatestTopic);
//				Assert.AreEqual(newPost3.Id, second.LatestPostId);
//				Assert.AreEqual(newPost3.Id, forum.LatestPostId);

//				postService.Update(newPost3, PostState.Quarantined);
//				Assert.AreEqual(null, forum.LatestTopic);
//				Assert.AreEqual(newPost2.Id, second.LatestPostId);
//				Assert.AreEqual(newPost2.Id, forum.LatestPostId);
//			}
//		}

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
//	}
//}