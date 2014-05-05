using NForum.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace NForum.Core {

	public static class CustomPropertyHolderExtensions {

		private static void LoadProperties(ICustomPropertyHolder holder) {
			if (holder.CustomData == null) {
				if (!String.IsNullOrWhiteSpace(holder.CustomProperties)) {
					holder.CustomData = XDocument.Parse(holder.CustomProperties);
				}
				else {
					holder.CustomData = new XDocument(new XElement("CustomProperties"));
				}
			}
		}

		public static Dictionary<String, String> GetCustomProperties(this ICustomPropertyHolder holder) {
			LoadProperties(holder);
			return holder.CustomData.Root.Elements("CustomProperty").ToDictionary(e => e.Attribute("Name").Value, e => e.Value, StringComparer.OrdinalIgnoreCase);
		}

		/// <summary>
		/// Method for trying to get an Int32 value from the custom properties.
		/// </summary>
		/// <param name="holder">The property holder.</param>
		/// <param name="key">The key of the property.</param>
		/// <param name="value">The value of the property.</param>
		/// <returns>True if an Int32 value with the given key was found.</returns>
		public static Boolean TryGetCustomPropertyInt32(this ICustomPropertyHolder holder, String key, out Int32 value) {
			String v = holder.GetCustomPropertyString(key);
			return Int32.TryParse(v, out value);
		}

		/// <summary>
		/// Method for getting an Int32 value from a property holder.
		/// </summary>
		/// <param name="holder">The property holder.</param>
		/// <param name="key">The key of the property.</param>
		/// <returns>The value of the property.</returns>
		public static Int32 GetCustomPropertyInt32(this ICustomPropertyHolder holder, String key) {
			String value = holder.GetCustomPropertyString(key);

			Int32 output;
			if (Int32.TryParse(value, out output)) {
				return output;
			}
			return default(Int32);
		}

		/// <summary>
		/// Method for trying to get an DateTime value from the custom properties.
		/// </summary>
		/// <param name="holder">The property holder.</param>
		/// <param name="key">The key of the property.</param>
		/// <param name="value">The value of the property.</param>
		/// <returns>True if an DateTime value with the given key was found.</returns>
		public static Boolean TryGetCustomPropertyDateTime(this ICustomPropertyHolder holder, String key, out DateTime value) {
			String v = holder.GetCustomPropertyString(key);

			DateTime output;
			if (DateTime.TryParse(v, out value)) {
				value = new DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, value.Second, value.Millisecond, DateTimeKind.Utc);
				return true;
			}
			return false;
		}

		/// <summary>
		/// Method for getting an DateTime value from a property holder.
		/// </summary>
		/// <param name="holder">The property holder.</param>
		/// <param name="key">The key of the property.</param>
		/// <returns>The value of the property.</returns>
		public static DateTime GetCustomPropertyDateTime(this ICustomPropertyHolder holder, String key) {
			String value = holder.GetCustomPropertyString(key);

			DateTime output;
			if (DateTime.TryParse(value, out output)) {
				return new DateTime(output.Year, output.Month, output.Day, output.Hour, output.Minute, output.Second, output.Millisecond, DateTimeKind.Utc);
			}
			return default(DateTime);
		}

		public static Boolean GetCustomPropertyBoolean(this ICustomPropertyHolder holder, String key) {
			String value = holder.GetCustomPropertyString(key);

			Boolean output;
			if (Boolean.TryParse(value, out output)) {
				return output;
			}
			return default(Boolean);
		}

		public static String GetCustomPropertyString(this ICustomPropertyHolder holder, String key) {
			LoadProperties(holder);
			if (holder.CustomData.Elements("CustomProperty").Any()) {
				XElement property = holder.CustomData.Root.Elements("CustomProperty").Where(p => p.Attribute("Name") != null && p.Attribute("Name").Value == key).FirstOrDefault();
				if (property != null) {
					return property.Value;
				}
			}
			return String.Empty;
		}

		public static Boolean Exists(this ICustomPropertyHolder holder, String key) {
			LoadProperties(holder);
			return holder.CustomData.Root.Elements("CustomProperty").Any() &&
					holder.CustomData.Root.Elements("CustomProperty").Any(p => p.Attribute("Name") != null && p.Attribute("Name").Value == key);
		}

		public static void SetCustomProperty(this ICustomPropertyHolder holder, String key, Boolean value) {
			holder.SetCustomProperty(key, value.ToString());
		}

		public static void SetCustomProperty(this ICustomPropertyHolder holder, String key, DateTime value) {
			holder.SetCustomProperty(key, value.ToUniversalTime().ToString("yyyyMMdd hh:mm:ss"));
		}

		public static void SetCustomProperty(this ICustomPropertyHolder holder, String key, Int32 value) {
			holder.SetCustomProperty(key, value.ToString());
		}

		public static void SetCustomProperty(this ICustomPropertyHolder holder, String key, String value) {
			LoadProperties(holder);
			if (holder.CustomData.Root.Elements("CustomProperty").Any()) {
				XElement property = holder.CustomData.Root.Elements("CustomProperty").Where(p => p.Attribute("Name") != null && p.Attribute("Name").Value == key).FirstOrDefault();
				if (property == null) {
					holder.CustomData.Root.Add(new XElement("CustomProperty", new XAttribute("Name", key), new XCData(value)));
				}
				else {
					property.Value = value;
				}

				holder.CustomProperties = holder.CustomData.ToString();
			}
		}

		public static void RemoveProperty(this ICustomPropertyHolder holder, String key) {
			LoadProperties(holder);
			if (holder.CustomData.Root.Elements("CustomProperty").Any()) {
				XElement property = holder.CustomData.Root.Elements("CustomProperty").Where(p => p.Attribute("Name") != null && p.Attribute("Name").Value == key).FirstOrDefault();
				if (property != null) {
					property.Remove();
				}

				holder.CustomProperties = holder.CustomData.ToString();
			}
		}

		public static Dictionary<String, Object> CustomPropertiesToDictionary(this ICustomPropertyHolder holder) {
			// TODO:
			return new Dictionary<string, object>();
		}
	}
}