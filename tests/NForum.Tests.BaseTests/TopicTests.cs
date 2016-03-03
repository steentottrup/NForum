using Microsoft.VisualStudio.TestTools.UnitTesting;
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
	public class TopicTests {
		private static NForumDbContext dbContext;

		[ClassInitialize]
		public static void SetUp(TestContext context) {
			Initializer.Initialize();
		}

		[ClassCleanup]
		public static void TearDown() { }

		[TestMethod]
		public void CreateNewTopic() {

		}
	}
}
