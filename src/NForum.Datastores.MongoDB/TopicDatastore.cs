using System;
using NForum.Core.Dtos;
using NForum.Domain;

namespace NForum.Datastores.MongoDB {

	public class TopicDatastore : ITopicDatastore {
		protected readonly CommonDatastore datastore;

		public TopicDatastore(CommonDatastore datastore) {
			this.datastore = datastore;
		}

		public ITopicDto Create(Topic topic) {
			throw new NotImplementedException();
		}

		public void DeleteById(String id) {
			throw new NotImplementedException();
		}

		public ITopicDto Update(Topic topic) {
			throw new NotImplementedException();
		}
	}
}
