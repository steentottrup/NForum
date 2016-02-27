using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NForum.Core.Abstractions.Services;
using NForum.Core.Services;
using NForum.Core.Abstractions.Data;
using NForum.Core.Abstractions.Providers;
using NForum.Core.Abstractions.Events;
using NForum.Core;
using FluentAssertions;

namespace NForum.Tests.BaseTests {
	[TestClass]

	public class CategoryTests {

		private ICategoryService GetCategoryService() {
			// TODO: Change this!
			IDataStore dataStore = new FakeDataStore();
			IPermissionService permissionService = new PermissionService();
			ILoggingService loggingService = new LoggingService(new FakeLogger(), new FakeLogger());
			IUserProvider userProvider = new FakeUserProvider();
			IEventPublisher eventPublisher = new FakeEventPublisher();
			return new CategoryService(dataStore, permissionService, loggingService, userProvider, eventPublisher);
		}

		[TestMethod]
		public void CreateNewCategory() {
			ICategoryService catService = this.GetCategoryService();

			String name = "Category name";
			Int32 sortOrder = 1;

			String description = "the description";
			Category category = catService.Create(name, sortOrder, description);

			category.Name.Should().Be(name);
			category.SortOrder.Should().Be(sortOrder);
			category.Description.Should().Be(description);
		}

		[TestMethod]
		public void CreateNewCategoryWithException() {
			ICategoryService catService = this.GetCategoryService();

			String name = String.Empty;
			Int32 sortOrder = 1;
			String description = "the description";
			Action action = () => catService.Create(name, sortOrder, description);

			action.ShouldThrow<ArgumentNullException>();
		}
	}
}
