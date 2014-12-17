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
using StackExchange.Profiling;
using StackExchange.Profiling.EntityFramework6;
using System;
using System.Collections.Generic;

using IUserProvider = NForum.Core.Abstractions.Providers.IUserProvider;

namespace NForum.Tests.BasicStructure {

	[TestClass]
	public class ForumServiceTests {

		[TestInitialize]
		public void Init() {
			//MiniProfiler.Start();
			//MiniProfilerEF6.Initialize();
		}

		[TestCleanup]
		public void TearDown() {
			//String output = MiniProfiler.Current.RenderPlainText();
			//System.IO.File.WriteAllText(@"c:\temp\nforumprofiling.txt", output);
			//MiniProfiler.Stop();
		}

		private void GetForumService(UnitOfWork uow, out ICategoryService categoryService, out IForumService forumService) {
			ICategoryRepository cateRepo = new CategoryRepository(uow);
			IForumRepository forumRepo = new ForumRepository(uow);

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

			categoryService = new CategoryService(userProvider, cateRepo, eventPublisher, logger, permService);
			forumService = new ForumService(userProvider, cateRepo, forumRepo, eventPublisher, logger, permService);
		}

		[TestMethod]
		public void Creating() {
			using (UnitOfWork uow = new UnitOfWork()) {
				uow.BeginTransaction();
				IForumService forumService;
				ICategoryService categoryService;
				this.GetForumService(uow, out categoryService, out forumService);

				Category category = categoryService.Create("With child forum", "meh", 100);

				String name = "First one?";
				String description = "bla bla";
				Int32 sortOrder = 50;
				Forum forum = forumService.Create(category, name, description, sortOrder);

				uow.Commit();

				forum = forumService.Read(forum.Id);

				Assert.AreEqual(name, forum.Name);
				Assert.AreEqual(description, forum.Description);
				Assert.AreEqual(sortOrder, forum.SortOrder);
				Assert.AreEqual(category.Id, forum.CategoryId);
			}
		}

		//[TestMethod]
		//public void Updating() {
		//}
	}
}