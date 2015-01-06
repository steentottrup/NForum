using System;

namespace NForum.Demo.WebApi.Models {

	public abstract class CategoryBase {
		public String Name { get; set; }
		public String Description { get; set; }
		public Int32 SortOrder { get; set; }
	}
}