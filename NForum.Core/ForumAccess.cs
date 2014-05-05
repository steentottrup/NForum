using System;
using System.ComponentModel.DataAnnotations;

namespace NForum.Core {

	public class ForumAccess {
		public Int32 Id { get; set; }
		public Int32 ForumId { get; set; }
		public Int32 GroupId { get; set; }
		public Int32 AccessMaskId { get; set; }

		public Forum Forum { get; set; }
		public Group Group { get; set; }
		public AccessMask AccessMask { get; set; }
	}
}