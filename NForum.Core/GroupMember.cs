using System;
using System.ComponentModel.DataAnnotations;

namespace NForum.Core {

	public class GroupMember {
		public Int32 Id { get; set; }
		public Int32 UserId { get; set; }
		public Int32 GroupId { get; set; }

		public virtual Group Group { get; set; }
		public virtual User User { get; set; }
	}
}