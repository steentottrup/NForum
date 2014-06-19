using System;
using System.Data.Entity.Infrastructure;

namespace NForum.Persistence.EntityFramework {

	public static class DbEntityEntryExtensions {

		public static Boolean DateChanged(this DbEntityEntry entry, String propertyName, out DateTime? dt) {
			dt = entry.CurrentValues[propertyName] as DateTime?;
			if (dt.HasValue) {
				if (dt.Value.Kind != DateTimeKind.Utc && dt.Value.Kind != DateTimeKind.Unspecified) {
					dt = dt.Value.ToUniversalTime();
					return true;
				}
			}

			return false;
		}
	}
}