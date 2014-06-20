using NForum.Core.Abstractions.Providers;
using System;
using System.Globalization;

namespace NForum.Core.Providers {

	public class RegionalSettingsProvider : IRegionalSettingsProvider {

		public RegionalSettingsProvider(CultureInfo culture, TimeZoneInfo tzi) {
			this.LanguageRegion = culture;
			this.TimeZone = tzi;
		}

		public CultureInfo LanguageRegion { get; private set; }
		public TimeZoneInfo TimeZone { get; private set; }
	}
}