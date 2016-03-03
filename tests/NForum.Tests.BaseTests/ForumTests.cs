using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NForum.Core;
using NForum.Core.Abstractions.Data;
using NForum.Core.Abstractions.Events;
using NForum.Core.Abstractions.Providers;
using NForum.Core.Abstractions.Services;
using NForum.Core.Services;
using NForum.Database.EntityFramework;
using NForum.Database.EntityFramework.Repositories;
using NForum.Tests.Common;
using System;
using System.Data.Common;

namespace NForum.Tests.BaseTests {

	[TestClass]
	public class ForumTests {
		private static NForumDbContext dbContext;

		[ClassInitialize]
		public static void SetUp(TestContext context) {
			Initializer.Initialize();
		}

		[ClassCleanup]
		public static void TearDown() { }

		[TestMethod]
		public void CreateNewForum() {
			ICategoryService categoryService = Initializer.CategoryService;
			IForumService forumService = Initializer.ForumService;

			Category category = categoryService.Create("category", 1, String.Empty);

			String name = "Forum name";
			Int32 sortOrder = 1;

			String description = "the description";
			Forum forum = forumService.Create(category.Id, name, sortOrder, description);

			forum.Name.Should().Be(name);
			forum.SortOrder.Should().Be(sortOrder);
			forum.Description.Should().Be(description);
		}

		[TestMethod]
		public void CreateNewForumWithException() {
			ICategoryService categoryService = Initializer.CategoryService;
			IForumService forumService = Initializer.ForumService;

			Category category = categoryService.Create("category", 1, String.Empty);

			String name = String.Empty;
			Int32 sortOrder = 1;
			String description = "the description";
			Action action = () => forumService.Create(category.Id, name, sortOrder, description);

			action.ShouldThrow<ArgumentNullException>();
		}
	}
}
