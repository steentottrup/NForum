using System;
using System.Collections.Generic;

namespace NForum.Database.EntityFramework.Dbos {

	public class Category {
		public Guid Id { get; set; }
		public String Name { get; set; }
		public Int32 SortOrder { get; set; }
		public String Description { get; set; }
		public String CustomData { get; set; }

		public virtual ICollection<Forum> Forums { get; set; }
	}
}
