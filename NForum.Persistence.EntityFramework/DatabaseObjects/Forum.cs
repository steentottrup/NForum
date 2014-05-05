//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using Constants = NForum.Core.Constants;

//namespace NForum.Persistence.EntityFramework.DatabaseObjects {

//	public class Forum {
//		public Int32 Id { get; set; }
//		public Int32 CategoryId { get; set; }
//		public Int32? ParentForumId { get; set; }
//		public Int32? LatestTopicId { get; set; }
//		public Int32? LatestPostId { get; set; }
//		public String Name { get; set; }
//		public String Description { get; set; }
//		public Int32 SortOrder { get; set; }
//		public String CustomProperties { get; set; }

//		public virtual Category Category { get; set; }
//		public virtual Forum ParentForum { get; set; }
//		public virtual Topic LatestTopic { get; set; }
//		public virtual Post LatestPost { get; set; }

//		public virtual ICollection<Topic> Topics { get; set; }
//	}
//}