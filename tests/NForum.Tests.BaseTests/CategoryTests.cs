using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NForum.Core.Abstractions.Services;
using NForum.Core.Services;
using NForum.Core.Abstractions.Data;
using NForum.Core.Abstractions.Providers;
using NForum.Core.Abstractions.Events;
using NForum.Core;
using FluentAssertions;
using NForum.Database.EntityFramework.Repositories;
using System.Data.Common;
using NForum.Database.EntityFramework;
using NForum.Tests.Common;

namespace NForum.Tests.BaseTests {

	[TestClass]
	public class CategoryTests {
		private static NForumDbContext dbContext;

		[ClassInitialize]
		public static void SetUp(TestContext context) {
			Initializer.Initialize();
		}

		[ClassCleanup]
		public static void TearDown() { }

		[TestMethod]
		public void CreateNewCategory() {
			ICategoryService catService = Initializer.CategoryService;

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
			ICategoryService catService = Initializer.CategoryService;

			String name = String.Empty;
			Int32 sortOrder = 1;
			String description = "the description";
			Action action = () => catService.Create(name, sortOrder, description);

			action.ShouldThrow<ArgumentNullException>();
		}
	}
}
