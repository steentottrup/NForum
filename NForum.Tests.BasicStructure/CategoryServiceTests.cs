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
using NForum.Persistence.EntityFramework.Migrations;
using NForum.Persistence.EntityFramework.Repositories;
using NForum.Tests.CommonMocks;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace NForum.Tests.BasicStructure {

	[TestClass]
	public class CategoryServiceTests {
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

		private ICategoryService GetCategoryService(UnitOfWork uow) {
			ICategoryRepository cateRepo = new CategoryRepository(uow);

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

			return new CategoryService(userProvider, cateRepo, eventPublisher, logger, permService);
		}

		[TestMethod]
		public void CreatingCategory() {
			ICategoryService categoryService = this.GetCategoryService(uow);

			String name = "first category";
			String description = "the very first category";
			Int32 sortOrder = 10;
			Category category = categoryService.Create(name, description, sortOrder);

			category = categoryService.Read(category.Id);

			Assert.AreEqual(name, category.Name);
			Assert.AreEqual(description, category.Description);
			Assert.AreEqual(sortOrder, category.SortOrder);
		}

		[TestMethod]
		public void UpdatingCategory() {
			ICategoryService categoryService = this.GetCategoryService(uow);

			String name = "first category";
			String description = "the very first category";
			Int32 sortOrder = 10;
			Category category = categoryService.Create(name, description, sortOrder);

			String updatedName = "Actually the second";
			String updatedDescription = "The second!";
			Int32 updatedSortOrder = 20;

			category.Name = updatedName;
			category.Description = updatedDescription;
			category.SortOrder = updatedSortOrder;
			category = categoryService.Update(category);

			Assert.AreEqual(updatedName, category.Name);
			Assert.AreEqual(updatedDescription, category.Description);
			Assert.AreEqual(updatedSortOrder, category.SortOrder);
		}
	}
}