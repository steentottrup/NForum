using NForum.Core;
using System;
using System.Data.Entity.ModelConfiguration;

namespace NForum.Persistence.EntityFramework.Configurations {

	public class CategoryConfiguration : EntityTypeConfiguration<Category> {

		public CategoryConfiguration() {
			this.ToTable(DatabaseConfiguration.Instance.GetTableName(typeof(Category)));
			this.HasKey(c => c.Id);
			this.Property(c => c.Name).IsRequired().HasMaxLength(Constants.FieldLengths.CategoryName);
			this.Property(c => c.Description).HasMaxLength(Int32.MaxValue);
			this.Property(c => c.SortOrder).IsRequired();
			this.Property(c => c.CustomProperties).HasMaxLength(Int32.MaxValue);
			this.Ignore(b => b.CustomData);
			// A category can have many forums
			//this.HasMany(c => c.Forums).WithRequired(f => f.Category);
		}
	}
}