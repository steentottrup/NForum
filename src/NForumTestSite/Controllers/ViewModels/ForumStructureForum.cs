using System;
using System.Collections.Generic;

namespace NForumTestSite.Controllers.ViewModels {

	public class ForumStructureForum {
		public String Id { get; set; }
		public String Name { get; set; }
		public Int32 SortOrder { get; set; }
		public IEnumerable<ForumStructureForum> Forums { get; set; }
	}
}
