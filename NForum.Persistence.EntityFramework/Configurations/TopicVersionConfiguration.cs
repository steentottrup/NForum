using NForum.Core;
using System;
using System.Data.Entity.ModelConfiguration;

namespace NForum.Persistence.EntityFramework.Configurations {

	public class TopicVersionConfiguration : EntityTypeConfiguration<TopicVersion> {

		public TopicVersionConfiguration() {
			this.ToTable(DatabaseConfiguration.Instance.GetTableName(typeof(TopicVersion)));
			this.HasKey(tv => tv.Id);
			this.Property(tv => tv.TopicId).IsRequired();
			this.Property(tv => tv.ForumId).IsRequired();
			this.Property(tv => tv.AuthorId).IsRequired();
			this.Property(tv => tv.EditorId).IsRequired();
			this.Property(tv => tv.Created).IsRequired();
			this.Property(tv => tv.Changed).IsRequired();
			this.Property(tv => tv.State).IsRequired();
			this.Property(tv => tv.Type).IsRequired();
			this.Property(tv => tv.Subject).IsRequired().HasMaxLength(Int32.MaxValue);
			this.Property(tv => tv.Message).HasMaxLength(Int32.MaxValue);
			this.Property(tv => tv.CustomProperties).HasMaxLength(Int32.MaxValue);
			this.Ignore(b => b.CustomData);
		}
	}
}