using Newtonsoft.Json.Linq;
using System;

namespace NForum.Core.Abstractions {

	public interface ICustomPropertyHolder {
		JObject CustomProperties { get; set; }
		String CustomData { get; set; }
	}
}
