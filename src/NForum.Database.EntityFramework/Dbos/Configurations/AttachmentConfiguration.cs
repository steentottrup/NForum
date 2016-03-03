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
			this.Property(c => c.Created).IsRequired();
			this.Property(c => c.DownloadCount).IsOptional();
			this.Property(c => c.FileType).IsRequired().HasMaxLength(10/* TODO: */);
			this.Property(c => c.OriginalFilename).IsRequired().HasMaxLength(1000/* TODO: */);
			this.Property(c => c.Size).IsRequired();
			this.Property(c => c.Url).IsRequired().HasMaxLength(Int32.MaxValue);
		}
	}
}
