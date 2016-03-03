using Microsoft.VisualStudio.TestTools.UnitTesting;
using NForum.Core.Abstractions.Data;
using NForum.Core.Abstractions.Events;
using NForum.Core.Abstractions.Providers;
using NForum.Core.Abstractions.Services;
using NForum.Core.Services;
using System;

namespace NForum.Tests.BaseTests {

	[TestClass]
	public class TopicTests {

		private ITopicService GetTopicService() {
			// TODO: Change this!
			IDataStore dataStore = new FakeDataStore();
			IPermissionService permissionService = new PermissionService();
			ILoggingService loggingService = new LoggingService(new FakeLogger(), new FakeLogger());
			IUserProvider userProvider = new FakeUserProvider();
			IEventPublisher eventPublisher = new FakeEventPublisher();
			return new TopicService(dataStore, permissionService, loggingService, userProvider, eventPublisher);
		}

		[TestMethod]
		public void CreateNewTopic() {

		}
	}
}
