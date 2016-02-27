using System;
using System.Collections.Generic;

namespace NForum.Database.EntityFramework.Dbos {

	public class Forum {
		public Guid Id { get; set; }
		public String Name { get; set; }
		public Int32 SortOrder { get; set; }
		public String Description { get; set; }
		public String CustomData { get; set; }
		public Int32 Level { get; set; }

		public Guid CategoryId { get; set; }
		public virtual Category Category { get; set; }
		public Guid? ParentForumId { get; set; }
		public virtual Forum ParentForum { get; set; }

		public virtual ICollection<Forum> SubForums { get; set; }
	}
}
