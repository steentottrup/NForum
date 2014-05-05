//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;

//namespace NForum.Persistence.EntityFramework.DatabaseObjects {
	
//	public class Topic {
//		public Int32 Id { get; set; }
//		[Required]
//		public Int32 ForumId { get; set; }
//		[Required]
//		public Int32 AuthorId { get; set; }
//		[Required]
//		public Int32 EditorId { get; set; }

//		public Int32? LatestPostId { get; set; }

//		[Required]
//		public DateTime Created { get; set; }
//		[Required]
//		public DateTime Changed { get; set; }

//		[Required]
//		public NForum.Core.TopicState State { get; set; }
//		[Required]
//		public NForum.Core.TopicType Type { get; set; }

//		[Required]
//		[StringLength(Int32.MaxValue)]
//		public String Subject { get; set; }
//		[StringLength(Int32.MaxValue)]
//		public String Message { get; set; }

//		[StringLength(Int32.MaxValue)]
//		public String CustomProperties { get; set; }

//		public virtual Forum Forum { get; set; }
//		public virtual User Author { get; set; }
//		public virtual User Editor { get; set; }
//			public virtual Post LatestPost { get; set; }

//		public virtual ICollection<Post> Posts { get; set; }
//	}
//}