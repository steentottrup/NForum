using System;
using System.Data.Entity.ModelConfiguration;

using FC = NForum.Core.ForumConfiguration;

namespace NForum.Persistence.EntityFramework.Configurations {

	public class ForumConfigurationConfiguration : EntityTypeConfiguration<FC>  {

		public ForumConfigurationConfiguration() {
			this.ToTable(DatabaseConfiguration.Instance.GetTableName(typeof(FC)));
			this.HasKey(fc => fc.Id);
			this.Property(u => u.CustomProperties).HasMaxLength(Int32.MaxValue);
			this.Ignore(fc => fc.CustomData);
		}
	}
}