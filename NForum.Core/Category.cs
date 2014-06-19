using NForum.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace NForum.Core {

	public class Category : INamedElement, ISortableElement, ICustomPropertyHolder,ICloneable {
		public Int32 Id { get; set; }
		public String Name { get; set; }
		public String Description { get; set; }
		public Int32 SortOrder { get; set; }

		/// <summary>
		/// Data container for custom properties. Do NOT read or write this property directly, use the extension methods.
		/// </summary>
		public String CustomProperties { get; set; }
		/// <summary>
		/// Xml container for custom properties. Do NOT read or write this property directly, use the extension methods.
		/// </summary>
		public XDocument CustomData { get; set; }

		public virtual ICollection<Forum> Forums { get; set; }

		public Object Clone() {
			return new Category {
				Id = this.Id,
				Name = this.Name,
				Description = this.Description,
				SortOrder = this.SortOrder,
				CustomProperties = this.CustomProperties
			};
		}
	}
}