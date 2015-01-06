using NForum.Core;
using System;
using System.Data.Entity.ModelConfiguration;

namespace NForum.Persistence.EntityFramework.Configurations {

	public class TopicTrackerConfiguration : EntityTypeConfiguration<TopicTracker> {

		public TopicTrackerConfiguration() {
			this.ToTable(DatabaseConfiguration.Instance.GetTableName(typeof(TopicTracker)));
			this.HasKey(tt => tt.Id);
			this.Property(tt => tt.UserId).IsRequired();
			this.Property(tt => tt.TopicId).IsRequired();
			this.Property(tt => tt.LastViewed).IsRequired();
		}
	}
}