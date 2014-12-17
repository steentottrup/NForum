using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NForum.Persistence.EntityFramework;
using NForum.Core.Abstractions.Data;
using NForum.Persistence.EntityFramework.Repositories;
using NForum.Tests.CommonMocks;
using NForum.Core.Abstractions;
using NForum.Core;
using System.Collections.Generic;
using NForum.Core.Abstractions.Events;
using NForum.Core.EventSubscribers;
using NForum.Core.Abstractions.Services;
using NForum.Core.Abstractions.Providers;
using NForum.Core.Services;
using NForum.Core.Events;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using NForum.Persistence.EntityFramework.Migrations;
//using NUnit.Framework;

namespace NForum.Tests.BasicStructure {

	//[TestFixture]
	[TestClass]
	public class CategoryServiceTests {

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

		//[Test]
		[TestMethod]
		public void Creating() {
			Database.SetInitializer(new MigrateDatabaseToLatestVersion<UnitOfWork, MigrationConf>());

			using (UnitOfWork uow = new UnitOfWork()) {
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
		}

		[TestMethod]
		public void Updating() {
			using (UnitOfWork uow = new UnitOfWork()) {
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
}