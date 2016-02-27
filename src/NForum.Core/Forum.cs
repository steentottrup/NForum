using Newtonsoft.Json.Linq;
using NForum.Core.Abstractions;
using System;
using System.Collections.Generic;

namespace NForum.Core {

	public class Forum : ICustomPropertyHolder {
		public String Id { get; set; }
		public String Name { get; set; }
		public Int32 SortOrder { get; set; }

		public String CustomData { get; set; }
		public JObject CustomProperties { get; set; }

		public String CategoryId { get; set; }
		public String ParentForumId { get; set; }

		/// <summary>
		/// The parent category of the forum.
		/// </summary>
		/// <remarks>This property will always be null unless the object is fetched from a "structure" method</remarks>
		public Category Category { get; set; }
		/// <summary>
		/// The parent forum, if any, of the forum.
		/// </summary>
		/// <remarks>This property will always be null unless the object is fetched from a "structure" method</remarks>
		public Forum ParentForum { get; set; }
		/// <summary>
		/// The sub-forums, if any, of the forum
		/// </summary>
		/// <remarks>This property will always be empty unless the object is fetched from a "structure" method</remarks>
		public IEnumerable<Forum> SubForums { get; set; }
	}
}
