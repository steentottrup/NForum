using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace NForum.Core.Abstractions {

	public interface INameValueCollection : IEnumerable<KeyValuePair<String, String>> {
		String this[String key] { get; }
		IEnumerable<String> GetValues(String key);

		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get", Justification = "We're matching the name value collection API for compatibility")]
		String Get(String key);
	}
}