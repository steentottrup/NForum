using NForum.Core;
using System;
using System.Data.Entity.ModelConfiguration;

namespace NForum.Persistence.EntityFramework.Configurations {

	public class ForumTrackerConfiguration : EntityTypeConfiguration<ForumTracker> {

		public ForumTrackerConfiguration() {
			this.ToTable(DatabaseConfiguration.Instance.GetTableName(typeof(ForumTracker)));
			this.HasKey(ft => ft.Id);
			this.Property(ft => ft.UserId).IsRequired();
			this.Property(ft => ft.ForumId).IsRequired();
			this.Property(ft => ft.LastViewed).IsRequired();
		}
	}
}