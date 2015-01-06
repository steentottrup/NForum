using NForum.Core;
using System;
using System.Data.Entity.ModelConfiguration;

namespace NForum.Persistence.EntityFramework.Configurations {

	public class PostVersionConfiguration : EntityTypeConfiguration<PostVersion> {

		public PostVersionConfiguration() {
			this.ToTable(DatabaseConfiguration.Instance.GetTableName(typeof(PostVersion)));
			this.HasKey(pv => pv.Id);
			this.Property(pv => pv.TopicId).IsRequired();
			this.Property(pv => pv.PostId).IsRequired();
			this.Property(pv => pv.AuthorId).IsRequired();
			this.Property(pv => pv.EditorId).IsRequired();
			this.Property(pv => pv.Created).IsRequired();
			this.Property(pv => pv.Changed).IsRequired();
			this.Property(pv => pv.State).IsRequired();
			this.Property(pv => pv.Subject).IsRequired().HasMaxLength(Int32.MaxValue);
			this.Property(pv => pv.Message).HasMaxLength(Int32.MaxValue);
			this.Property(pv => pv.CustomProperties).HasMaxLength(Int32.MaxValue);
			this.Ignore(pv => pv.CustomData);
		}
	}
}