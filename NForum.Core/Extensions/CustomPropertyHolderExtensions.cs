using NForum.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace NForum.Core {

	public static class CustomPropertyHolderExtensions {
		private const String propertyNodeName = "CustomProperty";
		private const String propertyName = "Name";
		private const String propertyType = "Type";

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
			if (holder.CustomData.Elements(propertyNodeName).Any()) {
				XElement property = holder.CustomData.Root.Elements(propertyNodeName).Where(p => p.Attribute(propertyName) != null && p.Attribute(propertyName).Value == key).FirstOrDefault();
				if (property != null) {
					return property.Value;
				}
			}
			return String.Empty;
		}

		public static Boolean CustomPropertyExists(this ICustomPropertyHolder holder, String key) {
			LoadProperties(holder);
			return holder.CustomData.Root.Elements(propertyNodeName).Any() &&
					holder.CustomData.Root.Elements(propertyNodeName).Any(p => p.Attribute(propertyName) != null && p.Attribute(propertyName).Value == key);
		}

		public static void SetCustomProperty(this ICustomPropertyHolder holder, String key, Boolean value) {
			holder.SetCustomProperty(key, value.ToString(), "bool");
		}

		public static void SetCustomProperty(this ICustomPropertyHolder holder, String key, DateTime value) {
			holder.SetCustomProperty(key, value.ToUniversalTime().ToString("yyyyMMdd hh:mm:ss"), "datetime");
		}

		public static void SetCustomProperty(this ICustomPropertyHolder holder, String key, Int32 value) {
			holder.SetCustomProperty(key, value.ToString(), "int");
		}

		public static void SetCustomProperty(this ICustomPropertyHolder holder, String key, String value) {
			holder.SetCustomProperty(key, value.ToString(), "string");
		}

		private static void SetCustomProperty(this ICustomPropertyHolder holder, String key, String value, String type) {
			LoadProperties(holder);
			if (holder.CustomData.Root.Elements(propertyNodeName).Any()) {
				XElement property = holder.CustomData.Root.Elements(propertyNodeName).Where(p => p.Attribute(propertyName) != null && p.Attribute(propertyName).Value == key).FirstOrDefault();
				if (property == null) {
					holder.CustomData.Root.Add(new XElement(propertyNodeName, new XAttribute(propertyName, key), new XAttribute(propertyType, type), new XCData(value)));
				}
				else {
					property.Value = value;
					property.Attribute(propertyType).Value = type;
				}

				holder.CustomProperties = holder.CustomData.ToString();
			}
		}

		public static void RemoveProperty(this ICustomPropertyHolder holder, String key) {
			LoadProperties(holder);
			if (holder.CustomData.Root.Elements(propertyNodeName).Any()) {
				XElement property = holder.CustomData.Root.Elements(propertyNodeName).Where(p => p.Attribute(propertyName) != null && p.Attribute(propertyName).Value == key).FirstOrDefault();
				if (property != null) {
					property.Remove();
				}

				holder.CustomProperties = holder.CustomData.ToString();
			}
		}

		public static Dictionary<String, Object> GetCustomProperties(this ICustomPropertyHolder holder) {
			Dictionary<String, Object> output = new Dictionary<String, Object>();
			LoadProperties(holder);
			foreach (XElement property in holder.CustomData.Root.Elements(propertyNodeName)) {
				String key = property.Attribute(propertyName).Value;
				String type = property.Attribute(propertyType).Value;
				switch (type) {
					case "int":
						output.Add(key, holder.GetCustomPropertyInt32(key));
						break;
					case "bool":
						output.Add(key, holder.GetCustomPropertyBoolean(key));
						break;
					case "string":
						output.Add(key, holder.GetCustomPropertyString(key));
						break;
					case "datetime":
						output.Add(key, holder.GetCustomPropertyDateTime(key));
						break;
					default:
						// TODO:
						throw new ApplicationException(String.Format("Unknown property type '{0}'", type));
				}
			}

			return output;
		}
	}
}