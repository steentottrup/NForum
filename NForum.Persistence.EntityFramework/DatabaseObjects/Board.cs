//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using Constants = NForum.Core.Constants;

//namespace NForum.Persistence.EntityFramework.DatabaseObjects {

//	public class Board {
//		public Int32 Id { get; set; }
//		public String Name { get; set; }
//		public String Description { get; set; }
//		public Int32 SortOrder { get; set; }
//		public String CustomProperties { get; set; }
//		public Int32 TopicsPerPage { get; set; }
//		public Int32 PostsPerPage { get; set; }

//		public virtual ICollection<Category> Categories { get; set; }
//	}
//}