using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NForum.Database.EntityFramework.Dbos.Configurations {

	public class ForumUserConfiguration : EntityTypeConfiguration<ForumUser> {

		public ForumUserConfiguration() {
			this.ToTable(DatabaseConfiguration.Instance.GetTableName(typeof(ForumUser)));
			this.HasKey(c => c.Id);
			this.Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
			this.Property(c => c.Username).IsRequired().HasMaxLength(250/* TODO */);
			this.Property(c => c.Username).IsOptional().HasMaxLength(250/* TODO */);
			this.Property(c => c.ExternalId).IsRequired().HasMaxLength(100/* TODO */);
			this.Property(c => c.EmailAddress).IsOptional().HasMaxLength(250/* TODO */);
			this.Property(c => c.Culture).IsOptional().HasMaxLength(50/* TODO */);
			this.Property(c => c.Timezone).IsOptional().HasMaxLength(50/* TODO */);
			this.Property(c => c.Deleted).IsRequired();
			this.Property(c => c.UseFullname).IsRequired();
		}
	}
}
