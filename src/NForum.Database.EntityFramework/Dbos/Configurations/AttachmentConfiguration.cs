using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NForum.Database.EntityFramework.Dbos.Configurations {

	public class AttachmentConfiguration : EntityTypeConfiguration<Attachment> {

		public AttachmentConfiguration() {
			this.ToTable(DatabaseConfiguration.Instance.GetTableName(typeof(Attachment)));
			this.HasKey(c => c.Id);
			this.Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
			this.Property(c => c.MessageId).IsRequired();
		}
	}
}
