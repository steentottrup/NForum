//using System;
//using System.ComponentModel.DataAnnotations;

//namespace NForum.Persistence.EntityFramework.DatabaseObjects {

//	public class Post {
//		public Int32 Id { get; set; }
//		[Required]
//		public Int32 TopicId { get; set; }
//		[Required]
//		public Int32 AuthorId { get; set; }
//		[Required]
//		public Int32 EditorId { get; set; }

//		public Int32? ParentPostId { get; set; }

//		[Required]
//		public DateTime Created { get; set; }
//		[Required]
//		public DateTime Changed { get; set; }

//		[Required]
//		public NForum.Core.PostState State { get; set; }

//		[Required]
//		[StringLength(Int32.MaxValue)]
//		public String Subject { get; set; }
//		[StringLength(Int32.MaxValue)]
//		public String Message { get; set; }

//		[StringLength(Int32.MaxValue)]
//		public String CustomProperties { get; set; }

//		public virtual Topic Topic { get; set; }
//		public virtual User Author { get; set; }
//		public virtual User Editor { get; set; }
//		public virtual Post ParentPost { get; set; }
//	}
//}