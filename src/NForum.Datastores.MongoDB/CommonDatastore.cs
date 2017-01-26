using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace NForum.Datastores.MongoDB {

	public class CommonDatastore {
		private readonly IMongoCollection<Dbos.Category> categories;
		private readonly IMongoCollection<Dbos.Forum> forums;
		private readonly IMongoCollection<Dbos.Topic> topics;

		public CommonDatastore(IMongoCollection<Dbos.Category> categories, IMongoCollection<Dbos.Forum> forums, IMongoCollection<Dbos.Topic> topics) {
			this.categories = categories;
			this.forums = forums;
			this.topics = topics;
		}

		public virtual Dbos.Forum CreateForum(Dbos.Forum forum) {
			this.forums.InsertOne(forum);
			return forum;
		}

		public virtual Dbos.Topic CreateTopic(Dbos.Topic topic) {
			this.topics.InsertOne(topic);
			return topic;
		}

		public virtual Dbos.Category CreateCategory(Dbos.Category category) {
			this.categories.InsertOne(category);
			return category;
		}

		public virtual Dbos.Category ReadCategoryById(ObjectId id) {
			return this.categories.Find(c => c.Id == id).SingleOrDefault();
		}

		public virtual Dbos.Forum ReadForumById(ObjectId id) {
			return this.forums.Find(c => c.Id == id).SingleOrDefault();
		}

		public virtual Tuple<IEnumerable<Dbos.Category>, IEnumerable<Dbos.Forum>> ReadAllCategoriesAndForums() {
			IEnumerable<Dbos.Category> cats = this.categories.Find(d => true).ToList();
			IEnumerable<Dbos.Forum> fs = this.forums.Find(d => true).ToList();

			return new Tuple<IEnumerable<Dbos.Category>, IEnumerable<Dbos.Forum>>(cats, fs);
		}
	}
}
