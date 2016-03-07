using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace NForum.Database.EntityFramework.Dbos.Configurations {

	public class TopicConfiguration : EntityTypeConfiguration<Topic> {

		public TopicConfiguration() {
			this.ToTable(DatabaseConfiguration.Instance.GetTableName(typeof(Topic)));
			this.HasKey(c => c.Id);
			this.Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
			this.Property(c => c.Type).IsRequired();
			this.Property(c => c.State).IsRequired();
			this.Property(c => c.CustomData).HasMaxLength(Int32.MaxValue);
			this.Property(c => c.MessageId).IsRequired();
			this.Property(c => c.ForumId).IsRequired();
			this.Property(c => c.LatestReplyId).IsOptional();

			// TODO: Indexes!!
		}
	}
}
