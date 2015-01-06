using NForum.Core;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace NForum.Persistence.EntityFramework.Configurations {

	public class ForumConfiguration : EntityTypeConfiguration<Forum> {

		public ForumConfiguration() {
			this.ToTable(DatabaseConfiguration.Instance.GetTableName(typeof(Forum)));
			this.HasKey(f => f.Id);
			this.Property(f => f.CategoryId).IsRequired();
			this.Property(f => f.Name).IsRequired().HasMaxLength(Constants.FieldLengths.ForumName);
			this.Property(f => f.Description).HasMaxLength(Int32.MaxValue);
			this.Property(f => f.SortOrder).IsRequired();
			this.Property(f => f.CustomProperties).HasMaxLength(Int32.MaxValue);
			this.Ignore(b => b.CustomData);
			this.HasOptional(x => x.ParentForum);
			// A forum can have many topics!
			this.HasMany(x => x.Topics).WithRequired(i => i.Forum);
		}
	}
}