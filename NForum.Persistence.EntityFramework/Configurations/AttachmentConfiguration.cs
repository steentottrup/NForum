using NForum.Core;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace NForum.Persistence.EntityFramework.Configurations {

	public class AttachmentConfiguration : EntityTypeConfiguration<Attachment> {

		public AttachmentConfiguration() {
			this.ToTable(DatabaseConfiguration.Instance.GetTableName(typeof(Attachment)));
			this.HasKey(a => a.Id);
			this.Property(a => a.Filename).IsRequired().HasMaxLength(Constants.FieldLengths.Filename);
			this.Property(a => a.Path).IsRequired().HasMaxLength(Constants.FieldLengths.Path);
			this.Property(a => a.PostId).IsRequired();
			this.Property(a => a.AuthorId).IsRequired();
			this.Property(a => a.Size).IsRequired();
			this.Property(a => a.Created).IsRequired();
		}
	}
}