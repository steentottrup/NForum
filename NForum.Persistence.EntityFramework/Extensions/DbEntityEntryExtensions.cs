using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace NForum.Persistence.EntityFramework {

	public static class DbEntityEntryExtensions {

		public static void DateChange(this DbEntityEntry entry, String propertyName) {
			if (entry.State == EntityState.Added || entry.CurrentValues[propertyName] != entry.OriginalValues[propertyName]) {
				DateTime? dt = entry.CurrentValues[propertyName] as DateTime?;
				if (dt.HasValue) {
					// Already UTC?
					if (dt.Value.Kind != DateTimeKind.Utc) {
						// Nope, we need to change the data then!
						if (dt.Value.Kind == DateTimeKind.Unspecified) {
							// Unspecified? Probably a newly created DateTime without providing a Kind parameter!
							dt = new DateTime(dt.Value.Ticks, DateTimeKind.Local);
						}
						dt = dt.Value.ToUniversalTime();
						entry.CurrentValues[propertyName] = dt;
					}
				}
			}
		}
	}
}