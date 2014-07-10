using Microsoft.VisualStudio.TestTools.UnitTesting;
using NForum.Core;
using NForum.Core.Abstractions;
using NForum.Core.Abstractions.Data;
using NForum.Core.Abstractions.Events;
using NForum.Core.Abstractions.Providers;
using NForum.Core.Abstractions.Services;
using NForum.Core.Events;
using NForum.Core.EventSubscribers;
using NForum.Core.Services;
using NForum.Persistence.EntityFramework;
using NForum.Persistence.EntityFramework.Repositories;
using NForum.Tests.CommonMocks;
using System;
using System.Collections.Generic;

namespace NForum.Tests.BasicStructure {

	[TestClass]
	public class LatestPostTopicTests {

		private void GetTopicService(UnitOfWork uow, out ICategoryService categoryService, out IForumService forumService, out ITopicService topicService) {
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
			subscribers.Add(new ForumLatestEventSubscriber(forumRepo, topicRepo, postRepo, logger));

			IEventPublisher eventPublisher = new EventPublisher(subscribers, logger, request);
			IUserProvider userProvider = new DummyUserProvider(user);
			IPermissionService permService = new PermissionService();

			IForumConfigurationService confService = new ForumConfigurationService(confRepo);

			categoryService = new CategoryService(userProvider, cateRepo, eventPublisher, logger, permService);
			forumService = new ForumService(userProvider, cateRepo, forumRepo, eventPublisher, logger, permService);
			topicService = new TopicService(userProvider, forumRepo, topicRepo, eventPublisher, logger, permService, confService);
		}


		[TestMethod]
		public void LatestTopics() {
			using (UnitOfWork uow = new UnitOfWork()) {
				ICategoryService categoryService;
				IForumService forumService;
				ITopicService topicService;
				this.GetTopicService(uow, out categoryService, out forumService, out topicService);

				Category category = categoryService.Create("Latest test category", "meh", 100);
				Forum forum = forumService.Create(category, "Latest test forum", "meh", 101);

				Topic first = topicService.Create(forum, "The first one", "bla bla bla");
				Assert.AreEqual(first.Id, forum.LatestTopicId, "first post latest");

				Topic second = topicService.Create(forum, "The second one", "bla bla bla");
				Assert.AreEqual(second.Id, forum.LatestTopicId, "second post latest");

				topicService.Update(second, TopicState.Quarantined);
				Assert.AreEqual(first.Id, forum.LatestTopicId, "first post latest again");
			}
		}
	}
}