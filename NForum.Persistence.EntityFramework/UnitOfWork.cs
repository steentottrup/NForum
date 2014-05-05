using NForum.Core;
using NForum.Core.Abstractions.Data;
using System;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace NForum.Persistence.EntityFramework {

	public class UnitOfWork : DbContext {
		private ObjectContext objectContext;
		private DbTransaction transaction;

		public IDbSet<Forum> Forums { get; set; }
		public IDbSet<Board> Boards { get; set; }
		public IDbSet<Category> Categories { get; set; }
		public IDbSet<Topic> Topics { get; set; }
		public IDbSet<Post> Posts { get; set; }
		public IDbSet<User> Users { get; set; }

		public UnitOfWork()
			: base("Default") {
		}

		//public Int32 SaveChanges() {
		//	return base.SaveChanges();
		//}

		public IRepository<TEntity> Repository<TEntity>() where TEntity : class {
			throw new NotImplementedException();
		}

		public void BeginTransaction() {
			this.objectContext = ((IObjectContextAdapter)this).ObjectContext;
			if (this.objectContext.Connection.State != ConnectionState.Open) {
				this.objectContext.Connection.Open();
				this.transaction = this.objectContext.Connection.BeginTransaction();
			}
		}

		public Boolean Commit() {
			this.transaction.Commit();
			return true;
		}

		public void Rollback() {
			this.transaction.Rollback();
			// TODO:
			//((DataContext)_dataContext).SyncObjectsStatePostCommit();
		}

		public new void Dispose() {
			if (this.objectContext != null && this.objectContext.Connection.State == ConnectionState.Open) {
				this.objectContext.Connection.Close();
			}

			base.Dispose(true);
			GC.SuppressFinalize(this);
		}

		public new void Dispose(Boolean disposing) {
			base.Dispose(disposing);
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder) {
			base.OnModelCreating(modelBuilder);

			// We don't want pluralized table names!
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
			// No default cascaded deleted on one-to-many!
			modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

			modelBuilder.Entity<Board>().HasKey(b => b.Id);
			modelBuilder.Entity<Board>().Property(b => b.Name).IsRequired().HasMaxLength(Constants.FieldLengths.BoardName);
			modelBuilder.Entity<Board>().Property(b => b.Description).HasMaxLength(Int32.MaxValue);
			modelBuilder.Entity<Board>().Property(b => b.SortOrder).IsRequired();
			modelBuilder.Entity<Board>().Property(b => b.CustomProperties).HasMaxLength(Int32.MaxValue);
			modelBuilder.Entity<Board>().Ignore(b => b.CustomData);
			modelBuilder.Entity<Board>().Property(b => b.TopicsPerPage).IsRequired();
			modelBuilder.Entity<Board>().Property(b => b.PostsPerPage).IsRequired();
			// A board can have many categories
			modelBuilder.Entity<Board>().HasMany(x => x.Categories).WithRequired(i => i.Board);

			modelBuilder.Entity<Category>().HasKey(c => c.Id);
			modelBuilder.Entity<Category>().Property(c => c.BoardId).IsRequired();
			modelBuilder.Entity<Category>().Property(c => c.Name).IsRequired().HasMaxLength(Constants.FieldLengths.CategoryName);
			modelBuilder.Entity<Category>().Property(c => c.Description).HasMaxLength(Int32.MaxValue);
			modelBuilder.Entity<Category>().Property(c => c.SortOrder).IsRequired();
			modelBuilder.Entity<Category>().Property(c => c.CustomProperties).HasMaxLength(Int32.MaxValue);
			modelBuilder.Entity<Category>().Ignore(b => b.CustomData);
			// A category can have many boards
			modelBuilder.Entity<Category>().HasMany(c => c.Forums).WithRequired(f => f.Category);

			modelBuilder.Entity<Forum>().HasKey(f => f.Id);
			modelBuilder.Entity<Forum>().Property(f => f.CategoryId).IsRequired();
			modelBuilder.Entity<Forum>().Property(f => f.Name).IsRequired().HasMaxLength(Constants.FieldLengths.ForumName);
			modelBuilder.Entity<Forum>().Property(f => f.Description).HasMaxLength(Int32.MaxValue);
			modelBuilder.Entity<Forum>().Property(f => f.SortOrder).IsRequired();
			modelBuilder.Entity<Forum>().Property(f => f.CustomProperties).HasMaxLength(Int32.MaxValue);
			modelBuilder.Entity<Forum>().Ignore(b => b.CustomData);
			modelBuilder.Entity<Forum>().HasOptional(x => x.LatestPost);
			modelBuilder.Entity<Forum>().HasOptional(x => x.LatestTopic);
			modelBuilder.Entity<Forum>().HasOptional(x => x.ParentForum);
			// A forum can have many topics!
			modelBuilder.Entity<Forum>().HasMany(x => x.Topics).WithRequired(i => i.Forum);

			modelBuilder.Entity<Topic>().HasKey(f => f.Id);
			modelBuilder.Entity<Topic>().Property(f => f.ForumId).IsRequired();
			modelBuilder.Entity<Topic>().Property(f => f.AuthorId).IsRequired();
			modelBuilder.Entity<Topic>().Property(f => f.EditorId).IsRequired();
			modelBuilder.Entity<Topic>().HasOptional(x => x.LatestPost);
			modelBuilder.Entity<Topic>().Property(f => f.Created).IsRequired();
			modelBuilder.Entity<Topic>().Property(f => f.Changed).IsRequired();
			modelBuilder.Entity<Topic>().Property(f => f.State).IsRequired();
			modelBuilder.Entity<Topic>().Property(f => f.Type).IsRequired();
			modelBuilder.Entity<Topic>().Property(f => f.Subject).IsRequired().HasMaxLength(Int32.MaxValue);
			modelBuilder.Entity<Topic>().Property(f => f.Message).HasMaxLength(Int32.MaxValue);
			modelBuilder.Entity<Topic>().Property(f => f.CustomProperties).HasMaxLength(Int32.MaxValue);
			modelBuilder.Entity<Topic>().Ignore(b => b.CustomData);
			modelBuilder.Entity<Topic>().HasRequired(x => x.Author);
			modelBuilder.Entity<Topic>().HasRequired(x => x.Editor);
			modelBuilder.Entity<Topic>().HasRequired(x => x.Forum);
			modelBuilder.Entity<Topic>().HasOptional(x => x.LatestPost);
			// A topic can have many posts.
			modelBuilder.Entity<Topic>().HasMany(x => x.Posts).WithRequired(i => i.Topic);

			modelBuilder.Entity<Post>().HasKey(p => p.Id);
			modelBuilder.Entity<Post>().Property(f => f.TopicId).IsRequired();
			modelBuilder.Entity<Post>().Property(f => f.AuthorId).IsRequired();
			modelBuilder.Entity<Post>().Property(f => f.EditorId).IsRequired();
			modelBuilder.Entity<Post>().Property(f => f.Created).IsRequired();
			modelBuilder.Entity<Post>().Property(f => f.Changed).IsRequired();
			modelBuilder.Entity<Post>().Property(f => f.State).IsRequired();
			modelBuilder.Entity<Post>().Property(f => f.Subject).IsRequired().HasMaxLength(Int32.MaxValue);
			modelBuilder.Entity<Post>().Property(f => f.Message).HasMaxLength(Int32.MaxValue);
			modelBuilder.Entity<Post>().Property(f => f.CustomProperties).HasMaxLength(Int32.MaxValue);
			modelBuilder.Entity<Post>().Ignore(b => b.CustomData);
			modelBuilder.Entity<Post>().HasRequired(x => x.Author);
			modelBuilder.Entity<Post>().HasRequired(x => x.Editor);
			modelBuilder.Entity<Post>().HasRequired(x => x.Topic);
			modelBuilder.Entity<Post>().HasOptional(x => x.ParentPost);

			modelBuilder.Entity<User>().HasKey(u => u.Id);
			modelBuilder.Entity<User>().Property(u => u.Name).IsRequired().HasMaxLength(Constants.FieldLengths.UserName);
			modelBuilder.Entity<User>().Property(u => u.FullName).HasMaxLength(Constants.FieldLengths.FullName);
			modelBuilder.Entity<User>().Property(u => u.EmailAddress).IsRequired().HasMaxLength(Constants.FieldLengths.EmailAddress);
			modelBuilder.Entity<User>().Property(u => u.ProviderId).IsRequired().HasMaxLength(Constants.FieldLengths.ProviderId);
			modelBuilder.Entity<User>().Property(u => u.CustomProperties).HasMaxLength(Int32.MaxValue);
			modelBuilder.Entity<User>().Ignore(u => u.CustomData);

			modelBuilder.Entity<TopicReport>().HasKey(tr => tr.Id);
			modelBuilder.Entity<TopicReport>().HasRequired(tr => tr.Topic);
		}
	}
}