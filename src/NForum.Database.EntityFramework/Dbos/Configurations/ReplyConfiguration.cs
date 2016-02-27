using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NForum.Database.EntityFramework.Dbos.Configurations {

	public class ReplyConfiguration : EntityTypeConfiguration<Reply> {

		public ReplyConfiguration() {
			this.ToTable(DatabaseConfiguration.Instance.GetTableName(typeof(Reply)));
			this.HasKey(c => c.Id);
			this.Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
			this.Property(c => c.State).IsRequired();
			this.Property(c => c.CustomData).HasMaxLength(Int32.MaxValue);
			this.Property(c => c.MessageId).IsRequired();
			this.Property(c => c.TopicId).IsRequired();
			this.Property(c => c.ParentReplyId).IsOptional();
		}
	}
}
