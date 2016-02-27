using Newtonsoft.Json.Linq;
using NForum.Core.Abstractions;
using System;
using System.Linq;

namespace NForum.Core {

	public static class ICustomPropertyHolderExtensions {

		private static JToken GetProperty(ICustomPropertyHolder holder, String key) {
			LoadProperties(holder);
			return holder.CustomProperties.SelectToken(key);
		}

		private static void LoadProperties(ICustomPropertyHolder holder) {
			if (holder.CustomProperties == null) {
				if (!String.IsNullOrWhiteSpace(holder.CustomData)) {
					holder.CustomProperties = JObject.Parse(holder.CustomData);
				}
				else {
					holder.CustomProperties = new JObject();
				}
			}
		}

		// TODO:
	}
}
