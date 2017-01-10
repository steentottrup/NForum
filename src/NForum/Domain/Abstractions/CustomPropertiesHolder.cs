using NForum.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NForum.Domain.Abstractions {

	public abstract class CustomPropertiesHolder {
		private IDictionary<String, Object> properties = new Dictionary<String, Object>();

		protected CustomPropertiesHolder() { }

		protected CustomPropertiesHolder(ICustomPropertiesHolder data) {
			this.properties = data.CustomProperties;
			this.Id = data.Id;
		}

		public virtual void ClearCustomProperties() {
			this.properties.Clear();
		}

		public virtual void AddCustomProperty(String key, Object value) {
			if (this.properties.ContainsKey(key)) {
				this.properties[key] = value;
			}
			else {
				this.properties.Add(key, value);
			}
		}

		public virtual void RemoveCustomProperty(String key) {
			if (this.properties.ContainsKey(key)) {
				this.properties.Remove(key);
			}
		}

		public virtual ReadOnlyDictionary<String, Object> Properties {
			get {
				return new ReadOnlyDictionary<String, Object>(this.properties);
			}
		}

		public virtual String Id { get; protected set; }
	}
}
