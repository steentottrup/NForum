using System;

namespace NForumTestSite.Controllers.ViewModels {

	public class CreateCategoryModel {
		public String Name { get; set; }
		public String Description { get; set; }
		public Int32 SortOrder { get; set; }
	}
}
