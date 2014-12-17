using NForum.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection;

namespace NForum.Persistence.EntityFramework {

	public class UnitOfWork : DbContext {
		protected ObjectContext objectContext;
		protected DbTransaction transaction;

		public IDbSet<Attachment> Attachments { get; set; }
		public IDbSet<Category> Categories { get; set; }
		public IDbSet<FollowForum> FollowForums { get; set; }
		public IDbSet<FollowTopic> FollowTopics { get; set; }
		public IDbSet<ForumConfiguration> ForumConfigurations { get; set; }
		public IDbSet<Forum> Forums { get; set; }
		public IDbSet<ForumTracker> ForumTrackers { get; set; }
		public IDbSet<Post> Posts { get; set; }
		public IDbSet<Topic> Topics { get; set; }
		public IDbSet<TopicTracker> TopicTrackers { get; set; }
		public IDbSet<TopicVersion> TopicVersions { get; set; }
		public IDbSet<User> Users { get; set; }

		public UnitOfWork() : this("DefaultConnection") { }
		public UnitOfWork(String nameOrConnectionString)
			: base(nameOrConnectionString) {

			if (DatabaseConfiguration.Instance.HandleDates) {
				((IObjectContextAdapter)this).ObjectContext.ObjectMaterialized += ObjectContext_ObjectMaterialized;
				((IObjectContextAdapter)this).ObjectContext.SavingChanges += ObjectContext_SavingChanges;
			}
		}

		private void ObjectContext_SavingChanges(Object sender, EventArgs e) {
			IEnumerable<DbEntityEntry> changed = this.ChangeTracker.Entries().Where(en => en.State != EntityState.Unchanged);
			foreach (DbEntityEntry entry in changed) {
				Object entity = entry.Entity;
				// Get any DateTime properties!
				PropertyInfo[] props = entity.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(p => p.PropertyType == typeof(DateTime)).ToArray();
				foreach (PropertyInfo info in props) {
					entry.DateChange(info.Name);
				}

				// Get any nullable DateTime properties!
				props = entity.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(p => p.PropertyType == typeof(DateTime?)).ToArray();
				foreach (PropertyInfo info in props) {
					entry.DateChange(info.Name);
				}
			}
		}

		private void ObjectContext_ObjectMaterialized(Object sender, ObjectMaterializedEventArgs e) {
			// TODO: StopWatch!!
			Object entity = e.Entity;

			// Get any DateTime properties!
			PropertyInfo[] props = entity.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(p => p.PropertyType == typeof(DateTime)).ToArray();
			foreach (PropertyInfo info in props) {
				Object value = info.GetValue(entity);
				DateTime? dt = value as DateTime?;
				if (value != null) {
					// Let's fix the DateTime properties, it's straight out of the database,
					// so they should all be UTC
					info.SetValue(entity, dt.Value.FixTimeZone());
				}
			}

			// Get any nullable DateTime properties!
			props = entity.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(p => p.PropertyType == typeof(DateTime?)).ToArray();
			foreach (PropertyInfo info in props) {
				Object value = info.GetValue(entity);
				DateTime? dt = value as DateTime?;
				if (value != null) {
					// Let's fix the DateTime properties, it's straight out of the database,
					// so they should all be UTC
					info.SetValue(entity, dt.FixTimeZone());
				}
			}
		}

		public void BeginTransaction() {
			this.objectContext = ((IObjectContextAdapter)this).ObjectContext;
			if (this.objectContext.Connection.State != ConnectionState.Open) {
				this.objectContext.Connection.Open();
			}
			this.transaction = this.objectContext.Connection.BeginTransaction();
		}

		public Boolean Commit() {
			if (this.transaction == null) {
				// TODO:
				throw new ApplicationException("If you want to commit a transaction, you have to call BeginTransaction first!");
			}
			this.transaction.Commit();
			this.transaction = null;
			return true;
		}

		public void Rollback() {
			if (this.transaction == null) {
				// TODO:
				throw new ApplicationException("If you want to rollback a transaction, you have to call BeginTransaction first!");
			}
			this.transaction.Rollback();
			this.transaction = null;
			// TODO:
			//((DataContext)_dataContext).SyncObjectsStatePostCommit();
		}

		public new void Dispose() {
			if (this.objectContext != null && this.objectContext.Connection.State == ConnectionState.Open) {
				this.objectContext.Connection.Close();
			}
			if (this.transaction != null) {
				this.transaction.Rollback();
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

			// Let's apply all configurations!
			modelBuilder.Configurations.AddFromAssembly(typeof(UnitOfWork).Assembly);
		}
	}
}