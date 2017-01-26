using NForum.Core.Dtos;
using System;
using NForum.Core.Refs;
using System.Collections.Generic;

namespace NForum.Datastores.MongoDB.Dtos {

	public class Forum : IForumDto, IForumRef {
		public ICategoryRef Category { get; set; }

		public IDictionary<String, Object> CustomProperties { get; set; }

		public String Description { get; set; }

		public String Id { get; set; }

		public String Name { get; set; }

		public IForumRef ParentForum { get; set; }

		public Int32 SortOrder { get; set; }

		public IEnumerable<String> Path { get; set; }
	}
}
