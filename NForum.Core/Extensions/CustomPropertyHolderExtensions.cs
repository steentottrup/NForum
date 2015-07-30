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

		private const String int32Id = "int";
		private const String datetimeId = "datetime";
		private const String stringId = "string";
		private const String booleanId = "bool";

		private static XElement GetProperty(ICustomPropertyHolder holder, String key) {
			LoadProperties(holder);
			return holder.CustomData.Root.Elements(propertyNodeName).Where(p => p.Attribute(propertyName) != null && p.Attribute(propertyName).Value == key).FirstOrDefault();
		}

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

		private static void SetCustomProperty(this ICustomPropertyHolder holder, String key, String value, String type) {
			LoadProperties(holder);
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

		/// <summary>
		/// Method for trying to get an Int32 value from the custom properties.
		/// </summary>
		/// <param name="holder">The property holder.</param>
		/// <param name="key">The key of the property.</param>
		/// <param name="value">The value of the property.</param>
		/// <returns>True if an Int32 value with the given key was found.</returns>
		public static Boolean TryGetCustomPropertyInt32(this ICustomPropertyHolder holder, String key, out Int32 value) {
			XElement property = GetProperty(holder, key);
			value = 0;
			return property != null &&
					property.Attribute(propertyType).Value == int32Id &&
					Int32.TryParse(property.Value, out value);
		}

		/// <summary>
		/// Method for getting an Int32 value from a property holder.
		/// </summary>
		/// <param name="holder">The property holder.</param>
		/// <param name="key">The key of the property.</param>
		/// <returns>The value of the property.</returns>
		public static Int32 GetCustomPropertyInt32(this ICustomPropertyHolder holder, String key) {
			XElement property = GetProperty(holder, key);
			return Int32.Parse(property.Value);
		}

		/// <summary>
		/// Method for trying to get a DateTime value from the custom properties.
		/// </summary>
		/// <param name="holder">The property holder.</param>
		/// <param name="key">The key of the property.</param>
		/// <param name="value">The value of the property.</param>
		/// <returns>True if a DateTime value with the given key was found.</returns>
		public static Boolean TryGetCustomPropertyDateTime(this ICustomPropertyHolder holder, String key, out DateTime value) {
			XElement property = GetProperty(holder, key);

			value = DateTime.Now;
			Boolean success = property != null &&
					property.Attribute(propertyType).Value == datetimeId &&
					DateTime.TryParse(property.Value, out value);
			return success;
		}

		/// <summary>
		/// Method for getting a DateTime value from a property holder.
		/// </summary>
		/// <param name="holder">The property holder.</param>
		/// <param name="key">The key of the property.</param>
		/// <returns>The value of the property.</returns>
		public static DateTime GetCustomPropertyDateTime(this ICustomPropertyHolder holder, String key) {
			XElement property = GetProperty(holder, key);

			return DateTime.Parse(property.Value).ToUniversalTime();
		}

		/// <summary>
		/// Method for trying to get an Boolean value from the custom properties.
		/// </summary>
		/// <param name="holder">The property holder.</param>
		/// <param name="key">The key of the property.</param>
		/// <param name="value">The value of the property.</param>
		/// <returns>True if an Boolean value with the given key was found.</returns>
		public static Boolean TryGetCustomPropertyBoolean(this ICustomPropertyHolder holder, String key, out Boolean value) {
			XElement property = GetProperty(holder, key);
			value = false;
			return property != null &&
					property.Attribute(propertyType).Value == booleanId &&
					Boolean.TryParse(property.Value, out value);
		}

		public static Boolean GetCustomPropertyBoolean(this ICustomPropertyHolder holder, String key) {
			XElement property = GetProperty(holder, key);
			return Boolean.Parse(property.Value);
		}

		public static Boolean TryGetCustomPropertyString(this ICustomPropertyHolder holder, String key, out String value) {
			XElement property = GetProperty(holder, key);
			value = String.Empty;
			if (property != null && property.Attribute(propertyType).Value == stringId) {
				value = property.Value;
				return true;
			}
			return false;
		}

		public static String GetCustomPropertyString(this ICustomPropertyHolder holder, String key) {
			XElement property = GetProperty(holder, key);
			return property.Value;
		}

		public static Boolean CustomPropertyExists(this ICustomPropertyHolder holder, String key) {
			LoadProperties(holder);
			return holder.CustomData.Root.Elements(propertyNodeName).Any() &&
					holder.CustomData.Root.Elements(propertyNodeName).Any(p => p.Attribute(propertyName) != null && p.Attribute(propertyName).Value == key);
		}

		public static void SetCustomProperty(this ICustomPropertyHolder holder, String key, Boolean value) {
			holder.SetCustomProperty(key, value.ToString(), booleanId);
		}

		public static void SetCustomProperty(this ICustomPropertyHolder holder, String key, DateTime value) {
			holder.SetCustomProperty(key, value.ToUniversalTime().ToString("o"), datetimeId);
		}

		public static void SetCustomProperty(this ICustomPropertyHolder holder, String key, Int32 value) {
			holder.SetCustomProperty(key, value.ToString(), int32Id);
		}

		public static void SetCustomProperty(this ICustomPropertyHolder holder, String key, String value) {
			holder.SetCustomProperty(key, value, stringId);
		}

		public static void SetCustomProperties(this ICustomPropertyHolder holder, IDictionary<String, Object> properties) {
			if (properties != null && properties.Any()) {
				foreach (String key in properties.Keys) {
					Object value = properties[key];
					if (value is Int32) {
						holder.SetCustomProperty(key, (Int32)value);
					}
					else if (value is DateTime) {
						holder.SetCustomProperty(key, (DateTime)value);
					}
					else if (value is Boolean) {
						holder.SetCustomProperty(key, (Boolean)value);
					}
					else if (value is String) {
						holder.SetCustomProperty(key, (String)value);
					}
					else {
						holder.SetCustomProperty(key, value.ToString());
					}
				}
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
					case int32Id:
						Int32 valueInt32;
						if (holder.TryGetCustomPropertyInt32(key, out valueInt32)) {
							output.Add(key, valueInt32);
						}
						break;
					case booleanId:
						Boolean valueBoolean;
						if (holder.TryGetCustomPropertyBoolean(key, out valueBoolean)) {
							output.Add(key, valueBoolean);
						}
						break;
					case stringId:
						String valueString;
						if (holder.TryGetCustomPropertyString(key, out valueString)) {
							output.Add(key, output);
						}
						break;
					case datetimeId:
						DateTime valueDatetime;
						if (holder.TryGetCustomPropertyDateTime(key, out valueDatetime)) {
							output.Add(key, valueDatetime);
						}
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