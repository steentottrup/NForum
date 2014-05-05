using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System {

	public static class DateTimeExtensions {

		public static DateTime FixTimeZone(this DateTime dt) {
			if (dt.Kind == DateTimeKind.Unspecified) {
				return new DateTime(dt.Ticks, DateTimeKind.Utc);
			}
			else if (dt.Kind != DateTimeKind.Utc) {
				return TimeZoneInfo.ConvertTimeToUtc(dt);
			}
			return dt;
		}

		public static DateTime? FixTimeZone(this DateTime? dt) {
			if (dt.HasValue) {
				if (dt.Value.Kind == DateTimeKind.Unspecified) {
					return new DateTime(dt.Value.Ticks, DateTimeKind.Utc);
				}
				else if (dt.Value.Kind != DateTimeKind.Utc) {
					return TimeZoneInfo.ConvertTimeToUtc(dt.Value);
				}
			}
			return dt;
		}
	}
}