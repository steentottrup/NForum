using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NForum.Database.EntityFramework.Dbos.Configurations {

	public class MessageConfiguration : EntityTypeConfiguration<Message> {

		public MessageConfiguration() {
			this.ToTable(DatabaseConfiguration.Instance.GetTableName(typeof(Message)));
			this.HasKey(c => c.Id);
			this.Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
			this.Property(c => c.Subject).IsRequired().HasMaxLength(500 /* TODO */);
			this.Property(c => c.Text).HasMaxLength(Int32.MaxValue);
			this.Property(c => c.State).IsRequired();
			this.Property(c => c.Created).IsRequired();
			this.Property(c => c.Updated).IsRequired();
		}
	}
}
