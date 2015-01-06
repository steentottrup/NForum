using NForum.Core;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace NForum.Persistence.EntityFramework.Configurations {

	public class TopicConfiguration : EntityTypeConfiguration<Topic> {

		public TopicConfiguration() {
			this.ToTable(DatabaseConfiguration.Instance.GetTableName(typeof(Topic)));
			this.HasKey(f => f.Id);
			this.Property(f => f.ForumId).IsRequired();
			this.Property(f => f.AuthorId).IsRequired();
			this.Property(f => f.EditorId).IsRequired();
			this.Property(f => f.Created).IsRequired();
			this.Property(f => f.Changed).IsRequired();
			this.Property(f => f.State).IsRequired();
			this.Property(f => f.Type).IsRequired();
			this.Property(f => f.Subject).IsRequired().HasMaxLength(Int32.MaxValue);
			this.Property(f => f.Message).HasMaxLength(Int32.MaxValue);
			this.Property(f => f.CustomProperties).HasMaxLength(Int32.MaxValue);
			this.Ignore(b => b.CustomData);
			this.HasRequired(x => x.Author);
			this.HasRequired(x => x.Editor);
			this.HasRequired(x => x.Forum);
			// A topic can have many posts.
			this.HasMany(x => x.Posts).WithRequired(i => i.Topic);

			// Index for fetching the latest topic!
			String indexName = "ForumId_Created_State";
			this.Property(a => a.ForumId).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute(indexName, 1)));
			this.Property(a => a.Created).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute(indexName, 2)));
			this.Property(a => a.State).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute(indexName, 3)));
		}
	}
}