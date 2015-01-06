using NForum.Core;
using System;
using System.Data.Entity.ModelConfiguration;

namespace NForum.Persistence.EntityFramework.Configurations {

	public class UserConfiguration : EntityTypeConfiguration<User> {

		public UserConfiguration() {
			this.ToTable(DatabaseConfiguration.Instance.GetTableName(typeof(User)));
			this.HasKey(u => u.Id);
			this.Property(u => u.Name).IsRequired().HasMaxLength(Constants.FieldLengths.UserName);
			this.Property(u => u.FullName).HasMaxLength(Constants.FieldLengths.FullName);
			this.Property(u => u.EmailAddress).IsRequired().HasMaxLength(Constants.FieldLengths.EmailAddress);
			this.Property(u => u.ProviderId).IsRequired().HasMaxLength(Constants.FieldLengths.ProviderId);
			this.Property(u => u.CustomProperties).HasMaxLength(Int32.MaxValue);
			this.Property(u => u.Culture).IsRequired().HasMaxLength(Constants.FieldLengths.Culture);
			this.Property(u => u.TimeZone).IsRequired().HasMaxLength(Constants.FieldLengths.TimeZone);
			this.Ignore(u => u.CustomData);
		}
	}
}