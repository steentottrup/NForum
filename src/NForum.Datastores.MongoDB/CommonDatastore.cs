using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace NForum.Datastores.MongoDB {

	public class CommonDatastore {
		private readonly IMongoCollection<Dbos.Category> categories;
		private readonly IMongoCollection<Dbos.Forum> forums;
		private readonly IMongoCollection<Dbos.Topic> topics;
		private readonly IMongoCollection<Dbos.Reply> replies;

		public CommonDatastore(IMongoCollection<Dbos.Category> categories, IMongoCollection<Dbos.Forum> forums, IMongoCollection<Dbos.Topic> topics, IMongoCollection<Dbos.Reply> replies) {
			this.categories = categories;
			this.forums = forums;
			this.topics = topics;
			this.replies = replies;
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

		public virtual Dbos.Reply CreateReply(Dbos.Reply reply) {
			this.replies.InsertOne(reply);
			return reply;
		}

		public virtual Dbos.Category ReadCategoryById(ObjectId id) {
			return this.categories.Find(c => c.Id == id).SingleOrDefault();
		}

		public virtual Dbos.Forum ReadForumById(ObjectId id) {
			return this.forums.Find(c => c.Id == id).SingleOrDefault();
		}

		public virtual Dbos.Topic ReadTopicById(ObjectId id) {
			return this.topics.Find(t => t.Id == id).SingleOrDefault();
		}

		public virtual Dbos.Reply ReadReplyById(ObjectId id) {
			return this.replies.Find(r => r.Id == id).SingleOrDefault();
		}

		public virtual Tuple<IEnumerable<Dbos.Category>, IEnumerable<Dbos.Forum>> ReadAllCategoriesAndForums() {
			IEnumerable<Dbos.Category> cats = this.categories.Find(d => true).ToList();
			IEnumerable<Dbos.Forum> fs = this.forums.Find(d => true).ToList();

			return new Tuple<IEnumerable<Dbos.Category>, IEnumerable<Dbos.Forum>>(cats, fs);
		}
	}
}
