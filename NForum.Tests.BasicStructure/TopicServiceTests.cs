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

namespace NForum.Tests.BasicStructure {

	[TestClass]
	public class TopicServiceTests {
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

		private void GetTopicService(UnitOfWork uow, out ICategoryService categoryService, out IForumService forumService, out ITopicService topicService) {
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
		}

		[TestMethod]
		public void CreatingTopic() {
			uow.BeginTransaction();
			ITopicService topicService;
			IForumService forumService;
			ICategoryService categoryService;
			this.GetTopicService(uow, out categoryService, out forumService, out topicService);

			Category category = categoryService.Create("For first topic", "meh", 100);
			Forum forum = forumService.Create(category, "For first topic", "muh", 100);

			String subject = "First topic, evah!";
			String body = "A long topic body? Not really..";

			Topic topic = topicService.Create(forum, subject, body, TopicType.Regular);

			uow.Commit();

			topic = topicService.Read(topic.Id);

			topic.Subject.Should().Be(subject);
			topic.Message.Should().Be(body);
			topic.Type.Should().Be(TopicType.Regular);
		}

		[TestMethod]
		public void UpdatingTopic() {
			uow.BeginTransaction();
			ITopicService topicService;
			IForumService forumService;
			ICategoryService categoryService;
			this.GetTopicService(uow, out categoryService, out forumService, out topicService);

			Category category = categoryService.Create("For second topic", "meh", 100);
			Forum forum = forumService.Create(category, "For second topic", "muh", 100);

			String subject = "Second topic, evah!";
			String body = "A long topic body? Not really..";

			Topic topic = topicService.Create(forum, subject, body, TopicType.Regular);

			uow.Commit();

			String updatedSubject = "Updated topic, second one";
			String updatedBody = "So too long, let's shorten";

			topic.Subject = updatedSubject;
			topic.Message = updatedBody;
			topic = topicService.Update(topic);

			topic.Subject.Should().Be(updatedSubject);
			topic.Message.Should().Be(updatedBody);
			topic.Type.Should().Be(TopicType.Regular);
		}

		[TestMethod]
		public void UpdatingTopicType() {
			uow.BeginTransaction();
			ITopicService topicService;
			IForumService forumService;
			ICategoryService categoryService;
			this.GetTopicService(uow, out categoryService, out forumService, out topicService);

			Category category = categoryService.Create("For second topic", "meh", 100);
			Forum forum = forumService.Create(category, "For second topic", "muh", 100);
			Topic topic = topicService.Create(forum, "A regular topic", "blabla bla", TopicType.Regular);

			uow.Commit();

			topic.Type = TopicType.Sticky;
			topic = topicService.Update(topic);

			topic.Type.Should().Be(TopicType.Sticky);
		}
	}
}
