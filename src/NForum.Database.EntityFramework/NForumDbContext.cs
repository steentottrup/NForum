using System;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace NForum.Database.EntityFramework {

	public class NForumDbContext : DbContext {
		public IDbSet<Dbos.Category> Categories { get; set; }
		public IDbSet<Dbos.Forum> Forums { get; set; }
		public IDbSet<Dbos.Topic> Topics { get; set; }
		public IDbSet<Dbos.Message> Messages { get; set; }
		public IDbSet<Dbos.Attachment> Attachments { get; set; }
		public IDbSet<Dbos.Reply> Replies { get; set; }
		public IDbSet<Dbos.ForumUser> ForumUsers { get; set; }

		public NForumDbContext(DbConnection connection) : base(connection, true) { }
		public NForumDbContext() : this("DefaultConnection") { }
		public NForumDbContext(String nameOrConnectionString)
			: base(nameOrConnectionString) {
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder) {
			base.OnModelCreating(modelBuilder);

			// We don't want pluralized table names!
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
			// No default cascaded deleted on one-to-many!
			modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

			// Let's apply all configurations!
			modelBuilder.Configurations.AddFromAssembly(typeof(NForumDbContext).Assembly);
		}
	}
}
