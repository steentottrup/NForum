using System;
using System.Collections.Generic;

namespace NForum.Core.Dtos {

	public interface ICustomPropertiesHolder {
		IDictionary<String, Object> CustomProperties { get; }
		String Id { get; }
	}
}
