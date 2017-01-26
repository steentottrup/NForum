using NForum.Core.Dtos;
using System;
using System.Collections.Generic;

namespace NForum.CQS.Queries.Forums {

	public class ReadBreadcrumbForForum {
		public ICategoryDto Category { get; set; }
		public IEnumerable<IForumDto> Forums { get; set; }
	}
}
