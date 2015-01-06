using NForum.Core;
using System;
using System.Data.Entity.ModelConfiguration;

namespace NForum.Persistence.EntityFramework.Configurations {

	public class FollowTopicConfiguration : EntityTypeConfiguration<FollowTopic> {

		public FollowTopicConfiguration() {
			this.ToTable(DatabaseConfiguration.Instance.GetTableName(typeof(FollowTopic)));
			this.HasKey(ft => ft.Id);
			this.Property(ft => ft.UserId).IsRequired();
			this.Property(ft => ft.TopicId).IsRequired();
		}
	}
}