using System;

namespace NForum.Core.Abstractions.Providers {

	public interface ITextProvider {
		String Get(String ns, String key);
		String Get(String ns, String key, Object values);
		String Get(IRegionalSettingsProvider reginalSettings, String ns, String key);
		String Get(IRegionalSettingsProvider reginalSettings, String ns, String key, Object values);
	}
}