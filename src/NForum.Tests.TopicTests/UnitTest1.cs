using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NForum.Database.EntityFramework;
using System;
using System.Data.Common;

namespace NForum.Tests.TopicTests {

	[TestClass]
	public class UnitTest1 {
		private static NForumDbContext dbContext;

		[ClassInitialize]
		public static void SetUp(TestContext context) {
			DbConnection connection = Effort.DbConnectionFactory.CreateTransient();
			dbContext = new NForumDbContext(connection);
		}

		[ClassCleanup]
		public static void TearDown() {
			dbContext = null;
		}

		[TestMethod]
		public void TestMethod1() {



		}
	}
}
