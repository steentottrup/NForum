using System;

namespace NForumTestSite.Controllers.ViewModels {

	public class CreateCategoryModel {
		public readonly ForumStructure ForumStructure;

		public CreateCategoryModel() {
			this.ForumStructure = new ViewModels.ForumStructure();
		}

		public CreateCategoryModel(ForumStructure vm) {
			this.ForumStructure = vm;
		}

		public String Name { get; set; }
		public String Description { get; set; }
		public Int32 SortOrder { get; set; }
	}
}
