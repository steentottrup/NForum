using NForum.Core;
using System;
using System.Data.Entity.Migrations;

namespace NForum.Persistence.EntityFramework.Migrations {

	public class Indexes : DbMigration {

		public override void Up() {


			// Latest topic index
			this.CreateIndex(DatabaseConfiguration.Instance.GetTableName(typeof(Topic)), new String[] { "ForumId", "Created", "State" }, false);
			// Latest post index
			this.CreateIndex(DatabaseConfiguration.Instance.GetTableName(typeof(Post)), new String[] { "TopicId", "Created", "State" }, false);
			// User by provider index
			this.CreateIndex(DatabaseConfiguration.Instance.GetTableName(typeof(User)), "ProviderId", false);
		}
	}
}