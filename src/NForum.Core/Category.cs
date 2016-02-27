using Newtonsoft.Json.Linq;
using NForum.Core.Abstractions;
using System;
using System.Collections.Generic;

namespace NForum.Core {

	public class Category : ICustomPropertyHolder {
		public String Id { get; set; }
		public String Name { get; set; }
		public Int32 SortOrder { get; set; }
		public String Description { get; set; }

		public String CustomData { get; set; }
		public JObject CustomProperties { get; set; }

		/// <summary>
		/// The sub-forums of the category.
		/// </summary>
		/// <remarks>This property will always be empty unless the object is fetched from a "structure" method</remarks>
		public IEnumerable<Forum> Forums { get; set; }
	}
}
