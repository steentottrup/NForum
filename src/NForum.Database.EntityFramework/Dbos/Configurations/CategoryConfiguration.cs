using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NForum.Database.EntityFramework.Dbos.Configurations {
	public class CategoryConfiguration : EntityTypeConfiguration<Category> {

		public CategoryConfiguration() {
			this.ToTable(DatabaseConfiguration.Instance.GetTableName(typeof(Category)));
			this.HasKey(c => c.Id);
			this.Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
			this.Property(c => c.Name).IsRequired().HasMaxLength(250 /* TODO: */);
			this.Property(c => c.Description).HasMaxLength(Int32.MaxValue);
			this.Property(c => c.SortOrder).IsRequired();
			this.Property(c => c.CustomData).HasMaxLength(Int32.MaxValue);
		}
	}
}
