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
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace NForum.Tests.CustomProperties {

	[TestClass]
	public class TestCustomPropertiesHolders {
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

		private void GetPostService(UnitOfWork uow, out ICategoryService categoryService, out IForumService forumService, out ITopicService topicService, out IPostService postService) {
			ICategoryRepository cateRepo = new CategoryRepository(uow);
			IForumRepository forumRepo = new ForumRepository(uow);
			ITopicRepository topicRepo = new TopicRepository(uow);
			IPostRepository postRepo = new PostRepository(uow);
			IForumConfigurationRepository configRepo = new ForumConfigurationRepository(uow);

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
			IForumConfigurationService confService = new ForumConfigurationService(configRepo);

			categoryService = new CategoryService(userProvider, cateRepo, eventPublisher, logger, permService);
			forumService = new ForumService(userProvider, cateRepo, forumRepo, topicRepo, postRepo, eventPublisher, logger, permService);
			topicService = new TopicService(userProvider, forumRepo, topicRepo, postRepo, eventPublisher, logger, permService, confService);
			postService = new PostService(userProvider, forumRepo, topicRepo, postRepo, eventPublisher, logger, permService, confService);
		}

		[TestMethod]
		public void CategoryProperties() {
			ICategoryService categoryService;
			IForumService forumService;
			ITopicService topicService;
			IPostService postService;
			this.GetPostService(uow, out categoryService, out forumService, out topicService, out postService);

			String name = "category tested";
			String description = "the very first category";
			Int32 sortOrder = 10;

			IDictionary<String, Object> props = new Dictionary<String, Object>();
			String boolKey = "myBoolean";
			Boolean boolValue = true;
			props.Add(boolKey, boolValue);

			Category category = categoryService.Create(name, description, sortOrder, props);
			category = categoryService.Read(category.Id);

			category.GetCustomPropertyBoolean(boolKey).Should().Be(boolValue);
		}

		[TestMethod]
		public void ForumProperties() {
			ICategoryService categoryService;
			IForumService forumService;
			ITopicService topicService;
			IPostService postService;
			this.GetPostService(uow, out categoryService, out forumService, out topicService, out postService);

			Category category = categoryService.Create("category", "meh", 1);

			IDictionary<String, Object> props = new Dictionary<String, Object>();
			String boolKey = "myBoolean";
			Boolean boolValue = true;
			props.Add(boolKey, boolValue);

			Forum forum = forumService.Create(category, "forum for test", 1, props);
			forum = forumService.Read(forum.Id);

			forum.GetCustomPropertyBoolean(boolKey).Should().Be(boolValue);
		}

		[TestMethod]
		public void TopicProperties() {
			ICategoryService categoryService;
			IForumService forumService;
			ITopicService topicService;
			IPostService postService;
			this.GetPostService(uow, out categoryService, out forumService, out topicService, out postService);

			Category category = categoryService.Create("category", "desc", 1);

			Forum forum = forumService.Create(category, "forum", 1);

			String subject = "topic tested";
			String message = "the message";

			IDictionary<String, Object> props = new Dictionary<String, Object>();
			String boolKey = "myBoolean";
			Boolean boolValue = true;
			props.Add(boolKey, boolValue);

			Topic topic = topicService.Create(forum, subject, message, TopicType.Regular, props);
			topic = topicService.Read(topic.Id);

			topic.GetCustomPropertyBoolean(boolKey).Should().Be(boolValue);
		}

		[TestMethod]
		public void PostProperties() {
			ICategoryService categoryService;
			IForumService forumService;
			ITopicService topicService;
			IPostService postService;
			this.GetPostService(uow, out categoryService, out forumService, out topicService, out postService);

			Category category = categoryService.Create("category", "desc", 1);

			Forum forum = forumService.Create(category, "forum", 1);

			Topic topic = topicService.Create(forum, "subject", "message", TopicType.Regular);

			IDictionary<String, Object> props = new Dictionary<String, Object>();
			String boolKey = "myBoolean";
			Boolean boolValue = true;
			props.Add(boolKey, boolValue);

			Post post = postService.Create(topic, "subject", "message", props);

			post = postService.Read(post.Id);

			post.GetCustomPropertyBoolean(boolKey).Should().Be(boolValue);
		}

		// TODO: User, ForumConfiguration, AddonConfiguraion(s)
	}
}
