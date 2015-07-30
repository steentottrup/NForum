using FluentAssertions;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NForum.Core.Abstractions.Services;
using NForum.Persistence.EntityFramework;
using System.Data.Common;
using NForum.Core.Abstractions.Data;
using NForum.Core.Abstractions;
using NForum.Tests.CommonMocks;
using NForum.Persistence.EntityFramework.Repositories;
using NForum.Core;
using NForum.Core.Abstractions.Events;
using System.Collections.Generic;
using NForum.Core.Events;
using NForum.Core.Services;
using NForum.Core.Abstractions.Providers;

namespace NForum.Tests.CustomProperties {

	[TestClass]
	public class SetGetTests {

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
		public void SettingProperties() {
			ICategoryService categoryService = this.GetCategoryService(uow);

			String name = "first category";
			String description = "the very first category";
			Int32 sortOrder = 10;

			IDictionary<String, Object> props = new Dictionary<String, Object>();
			String stringKey = "myString";
			String stringValue = "This is my string value";
			props.Add(stringKey, stringValue);
			String intKey = "myInt32";
			Int32 intValue = 12345;
			props.Add(intKey, intValue);
			String boolKey = "myBoolean";
			Boolean boolValue = true;
			props.Add(boolKey, boolValue);
			String datetimeKey = "myDatetime";
			DateTime datetimeValue = DateTime.UtcNow;
			props.Add(datetimeKey, datetimeValue);

			Action a = () => categoryService.Create(name, description, sortOrder, props);

			a.ShouldNotThrow("nothing illegal happened");
		}

		[TestMethod]
		public void GettingProperties() {
			ICategoryService categoryService = this.GetCategoryService(uow);

			String name = "first category";
			String description = "the very first category";
			Int32 sortOrder = 10;

			IDictionary<String, Object> props = new Dictionary<String, Object>();
			String stringKey = "myString";
			String stringValue = "This is my string value";
			props.Add(stringKey, stringValue);
			String intKey = "myInt32";
			Int32 intValue = 12345;
			props.Add(intKey, intValue);
			String boolKey = "myBoolean";
			Boolean boolValue = true;
			props.Add(boolKey, boolValue);
			String datetimeKey = "myDatetime";
			DateTime datetimeValue = DateTime.UtcNow;
			props.Add(datetimeKey, datetimeValue);

			Category category = categoryService.Create(name, description, sortOrder, props);

			category = categoryService.Read(category.Id);

			category.GetCustomPropertyString(stringKey).Should().Be(stringValue);
			category.GetCustomPropertyInt32(intKey).Should().Be(intValue);
			category.GetCustomPropertyBoolean(boolKey).Should().Be(boolValue);
			category.GetCustomPropertyDateTime(datetimeKey).Should().Be(datetimeValue);
		}
	}
}
