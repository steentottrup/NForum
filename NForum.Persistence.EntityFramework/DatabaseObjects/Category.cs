//using NForum.Core;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using Constants = NForum.Core.Constants;

//namespace NForum.Persistence.EntityFramework.DatabaseObjects {

//	public class Category {
//		public Int32 Id { get; set; }
//		public Int32 BoardId { get; set; }
//		public String Name { get; set; }
//		public String Description { get; set; }
//		public Int32 SortOrder { get; set; }
//		public String CustomProperties { get; set; }

//		public virtual Board Board { get; set; }

//		public virtual ICollection<Forum> Forums { get; set; }
//	}
//}