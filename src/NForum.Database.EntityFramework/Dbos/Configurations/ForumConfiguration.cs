using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NForum.Database.EntityFramework.Dbos.Configurations {

	public class ForumConfiguration : EntityTypeConfiguration<Forum> {

		public ForumConfiguration() {
			this.ToTable(DatabaseConfiguration.Instance.GetTableName(typeof(Forum)));
			this.HasKey(c => c.Id);
			this.Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
			this.Property(c => c.Name).IsRequired().HasMaxLength(250 /* TODO: */);
			this.Property(c => c.Description).HasMaxLength(Int32.MaxValue);
			this.Property(c => c.SortOrder).IsRequired();
			this.Property(c => c.CustomData).HasMaxLength(Int32.MaxValue);
			this.Property(c => c.CategoryId).IsRequired();
			this.Property(c => c.ParentForumId).IsOptional();
			this.Property(c => c.Level).IsRequired();
		}
	}
}
