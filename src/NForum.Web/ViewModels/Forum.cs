using System;
using System.Collections.Generic;

namespace NForum.Web.ViewModels {

	public class Forum {
		public String Id { get; set; }
		public String Name { get; set; }
		public String Description { get; set; }
		// TODO: Change this!
		public ViewModels.Forum ParentForum { get; set; }
		public Core.Category Category { get; set; }
		public IEnumerable<Core.Forum> SubForums { get; set; }
		public IEnumerable<Core.Topic> Topics { get; set; }
	}
}
