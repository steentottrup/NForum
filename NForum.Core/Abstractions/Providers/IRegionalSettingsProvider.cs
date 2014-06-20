using System;
using System.Globalization;

namespace NForum.Core.Abstractions.Providers {

	public interface IRegionalSettingsProvider {
		CultureInfo LanguageRegion { get; }
		TimeZoneInfo TimeZone { get; }
	}
}