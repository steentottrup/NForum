using NForum.Core;
using System;
using System.Data.Entity.ModelConfiguration;

namespace NForum.Persistence.EntityFramework.Configurations {

	public class FollowForumConfiguration : EntityTypeConfiguration<FollowForum> {

		public FollowForumConfiguration() {
			this.ToTable(DatabaseConfiguration.Instance.GetTableName(typeof(FollowForum)));
			this.HasKey(ff => ff.Id);
			this.Property(ff => ff.UserId).IsRequired();
			this.Property(ff => ff.ForumId).IsRequired();
		}
	}
}
