using System;

namespace NForum.Api.Web.Models {

	public abstract class ForumBase {
		public String Name { get; set; }
		public String Description { get; set; }
		public Int32 SortOrder { get; set; }
	}
}
