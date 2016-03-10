using System;
using System.Collections.Generic;

namespace NForum.Web.ViewModels {

	public class Topic {
		public String Id { get; set; }
		public String Subject { get; set; }
		public String Text { get; set; }
		public ViewModels.Forum Forum { get; set; }
		public Core.Category Category { get; set; }

		public IEnumerable<NForum.Core.Reply> Replies { get; set; }
	}
}
